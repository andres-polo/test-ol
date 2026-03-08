import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { MatTableModule, MatTableDataSource } from '@angular/material/table';
import { MatPaginatorModule, PageEvent } from '@angular/material/paginator';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';
import { MatInputModule } from '@angular/material/input';
import { MatTooltipModule } from '@angular/material/tooltip';
import { FormsModule } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ComerciantesService, ComercianteDto } from '../../services/comerciantes.service';
import { AuthService } from '../../core/services/auth.service';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [
    CommonModule,
    RouterLink,
    MatTableModule,
    MatPaginatorModule,
    MatButtonModule,
    MatIconModule,
    MatFormFieldModule,
    MatSelectModule,
    MatInputModule,
    MatTooltipModule,
    FormsModule
  ],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss'
})
export class HomeComponent implements OnInit {
  dataSource = new MatTableDataSource<ComercianteDto>([]);
  displayedColumns = ['nombreRazonSocial', 'telefono', 'correo', 'fechaRegistro', 'cantidadEstablecimientos', 'estado', 'acciones'];
  totalCount = 0;
  page = 1;
  pageSize = 5;
  pageSizeOptions = [5, 10, 15];
  loading = false;
  filterNombre = '';
  filterEstado = '';

  constructor(
    private comerciantes: ComerciantesService,
    public auth: AuthService,
    private snackBar: MatSnackBar
  ) {}

  ngOnInit(): void {
    this.loadData();
  }

  loadData(): void {
    this.loading = true;
    this.comerciantes
      .getPaged(this.page, this.pageSize, {
        nombre: this.filterNombre || undefined,
        estado: this.filterEstado || undefined
      })
      .subscribe({
        next: (res) => {
          this.loading = false;
          if (res.success && res.data) {
            this.dataSource.data = res.data.items;
            this.totalCount = res.data.totalCount;
          }
        },
        error: () => {
          this.loading = false;
          this.snackBar.open('Error al cargar datos', 'Cerrar', { duration: 3000 });
        }
      });
  }

  onPageChange(e: PageEvent): void {
    this.page = e.pageIndex + 1;
    this.pageSize = e.pageSize;
    this.loadData();
  }

  onFilter(): void {
    this.page = 1;
    this.loadData();
  }

  toggleEstado(row: ComercianteDto): void {
    const nuevoEstado = row.estado === 'Activo' ? 'Inactivo' : 'Activo';
    this.comerciantes.patchEstado(row.id, nuevoEstado).subscribe({
      next: () => {
        this.snackBar.open('Estado actualizado', 'Cerrar', { duration: 2000 });
        this.loadData();
      },
      error: () => this.snackBar.open('Error al actualizar', 'Cerrar', { duration: 3000 })
    });
  }

  eliminar(row: ComercianteDto): void {
    if (!confirm(`¿Eliminar a ${row.nombreRazonSocial}?`)) return;
    this.comerciantes.delete(row.id).subscribe({
      next: () => {
        this.snackBar.open('Comerciante eliminado', 'Cerrar', { duration: 2000 });
        this.loadData();
      },
      error: () => this.snackBar.open('Error al eliminar', 'Cerrar', { duration: 3000 })
    });
  }

  descargarCsv(): void {
    this.comerciantes.downloadReporteCsv().subscribe({
      next: (blob) => {
        const url = URL.createObjectURL(blob);
        const a = document.createElement('a');
        a.href = url;
        a.download = `reporte-comerciantes-${new Date().toISOString().slice(0, 10)}.csv`;
        a.click();
        URL.revokeObjectURL(url);
        this.snackBar.open('Reporte descargado', 'Cerrar', { duration: 2000 });
      },
      error: () => this.snackBar.open('Error al descargar', 'Cerrar', { duration: 3000 })
    });
  }

  formatDate(d: string): string {
    return d ? new Date(d).toLocaleDateString('es-CO') : '-';
  }
}
