import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';

export interface ComercianteDto {
  id: number;
  nombreRazonSocial: string;
  municipio: string;
  telefono: string | null;
  correo: string | null;
  fechaRegistro: string;
  estado: string;
  cantidadEstablecimientos: number;
  totalIngresos: number;
  cantidadEmpleados: number;
}

export interface PagedResult<T> {
  items: T[];
  totalCount: number;
  page: number;
  pageSize: number;
}

export interface ComercianteCreateUpdateDto {
  nombreRazonSocial: string;
  municipioId: number;
  telefono?: string;
  correo?: string;
  fechaRegistro: string;
  estado: string;
}

export interface ApiResponse<T> {
  success: boolean;
  data?: T;
  message?: string;
}

@Injectable({ providedIn: 'root' })
export class ComerciantesService {
  private base = `${environment.apiUrl}/api/comerciantes`;

  constructor(private http: HttpClient) {}

  getPaged(page = 1, pageSize = 5, filters?: { nombre?: string; fechaDesde?: string; fechaHasta?: string; estado?: string }): Observable<ApiResponse<PagedResult<ComercianteDto>>> {
    let params = new HttpParams().set('page', page).set('pageSize', pageSize);
    if (filters?.nombre) params = params.set('nombre', filters.nombre);
    if (filters?.fechaDesde) params = params.set('fechaRegistroDesde', filters.fechaDesde);
    if (filters?.fechaHasta) params = params.set('fechaRegistroHasta', filters.fechaHasta);
    if (filters?.estado) params = params.set('estado', filters.estado);
    return this.http.get<ApiResponse<PagedResult<ComercianteDto>>>(this.base, { params });
  }

  getById(id: number): Observable<ApiResponse<ComercianteDto>> {
    return this.http.get<ApiResponse<ComercianteDto>>(`${this.base}/${id}`);
  }

  create(dto: ComercianteCreateUpdateDto): Observable<ApiResponse<ComercianteDto>> {
    return this.http.post<ApiResponse<ComercianteDto>>(this.base, dto);
  }

  update(id: number, dto: ComercianteCreateUpdateDto): Observable<ApiResponse<ComercianteDto>> {
    return this.http.put<ApiResponse<ComercianteDto>>(`${this.base}/${id}`, dto);
  }

  patchEstado(id: number, estado: string): Observable<ApiResponse<ComercianteDto>> {
    return this.http.patch<ApiResponse<ComercianteDto>>(`${this.base}/${id}/estado`, { estado });
  }

  delete(id: number): Observable<void> {
    return this.http.delete<void>(`${this.base}/${id}`);
  }

  downloadReporteCsv(): Observable<Blob> {
    return this.http.get(`${this.base}/reporte-csv`, { responseType: 'blob' });
  }
}
