import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatButtonModule } from '@angular/material/button';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ComerciantesService, ComercianteDto, ComercianteCreateUpdateDto } from '../../services/comerciantes.service';
import { MunicipiosService, MunicipioDto } from '../../services/municipios.service';
import { AuthService } from '../../core/services/auth.service';

@Component({
  selector: 'app-comerciante-form',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    RouterLink,
    MatCardModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatButtonModule,
    MatCheckboxModule
  ],
  templateUrl: './comerciante-form.component.html',
  styleUrl: './comerciante-form.component.scss'
})
export class ComercianteFormComponent implements OnInit {
  form: FormGroup;
  municipios: MunicipioDto[] = [];
  isEdit = false;
  id: number | null = null;
  loading = false;
  totalIngresos = 0;
  totalEmpleados = 0;
  poseeEstablecimientos = false;

  constructor(
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private comerciantes: ComerciantesService,
    private municipiosService: MunicipiosService,
    public auth: AuthService,
    private snackBar: MatSnackBar
  ) {
    this.form = this.fb.group({
      nombreRazonSocial: ['', [Validators.required, Validators.maxLength(200)]],
      municipioId: [null as number | null, [Validators.required, Validators.min(1)]],
      telefono: ['', Validators.maxLength(20)],
      correo: ['', Validators.email],
      fechaRegistro: [new Date().toISOString().slice(0, 10), Validators.required],
      estado: ['Activo', Validators.required],
      poseeEstablecimientos: [false]
    });
  }

  ngOnInit(): void {
    const idParam = this.route.snapshot.paramMap.get('id');

    this.municipiosService.getAll().subscribe({
      next: (res) => {
        if (res.success && res.data) this.municipios = res.data;
        if (idParam) this.loadComerciante(+idParam);
      }
    });

    if (!idParam) return;
    this.isEdit = true;
    this.id = +idParam;
  }

  private loadComerciante(id: number): void {
    this.comerciantes.getById(id).subscribe({
      next: (res) => {
        if (res.success && res.data) {
          const d = res.data;
          const municipioId = this.municipios.find((m) => m.nombre === d.municipio)?.id ?? null;
          this.form.patchValue({
            nombreRazonSocial: d.nombreRazonSocial,
            municipioId,
              telefono: d.telefono ?? '',
              correo: d.correo ?? '',
              fechaRegistro: d.fechaRegistro?.slice(0, 10),
              estado: d.estado
            });
            this.totalIngresos = d.totalIngresos;
            this.totalEmpleados = d.cantidadEmpleados;
            this.poseeEstablecimientos = d.cantidadEstablecimientos > 0;
          }
        }
      });
  }

  onSubmit(): void {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }
    const v = this.form.value;
    const fechaRegistro = typeof v.fechaRegistro === 'string'
      ? v.fechaRegistro
      : v.fechaRegistro ? new Date(v.fechaRegistro).toISOString().slice(0, 10) : '';
    const dto: ComercianteCreateUpdateDto = {
      nombreRazonSocial: v.nombreRazonSocial,
      municipioId: v.municipioId,
      telefono: v.telefono || undefined,
      correo: v.correo || undefined,
      fechaRegistro,
      estado: v.estado
    };

    this.loading = true;
    const req = this.isEdit && this.id
      ? this.comerciantes.update(this.id, dto)
      : this.comerciantes.create(dto);

    req.subscribe({
      next: () => {
        this.loading = false;
        this.snackBar.open(this.isEdit ? 'Actualizado' : 'Creado', 'Cerrar', { duration: 2000 });
        this.router.navigate(['/home']);
      },
      error: () => {
        this.loading = false;
        this.snackBar.open('Error al guardar', 'Cerrar', { duration: 3000 });
      }
    });
  }

  getMunicipioIdFromName(nombre: string): number | undefined {
    return this.municipios.find((m) => m.nombre === nombre)?.id;
  }
}
