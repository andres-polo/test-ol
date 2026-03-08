import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';

export interface MunicipioDto {
  id: number;
  nombre: string;
}

export interface ApiResponse<T> {
  success: boolean;
  data?: T;
}

@Injectable({ providedIn: 'root' })
export class MunicipiosService {
  constructor(private http: HttpClient) {}

  getAll(): Observable<ApiResponse<MunicipioDto[]>> {
    return this.http.get<ApiResponse<MunicipioDto[]>>(`${environment.apiUrl}/api/municipios`);
  }
}
