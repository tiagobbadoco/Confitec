import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class EscolaridadeService {
  private baseUrl: string = "";
  private endpoint: string = "escolaridade/";

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.baseUrl = baseUrl;
  }

  listar() {
    return this.http.get(this.baseUrl + this.endpoint);
  }

}
