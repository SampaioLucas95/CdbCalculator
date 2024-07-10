// src/app/calculate-investment/calculate-investment.service.ts
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class CalculateInvestmentService {
  private apiUrl = 'http://localhost:5001/api/v1/Cdb/calcular';

  constructor(private http: HttpClient) { }

  calculateInvestment(valorIncial: number, Meses: number) {
    const body = { ValorInicial: valorIncial, PrazoMeses: Meses };
    return this.http.post<any>(this.apiUrl, body);
  }
}
