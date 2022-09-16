import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { UsuarioModel } from '../interfaces/usuario-model';

@Injectable({
  providedIn: 'root'
})
export class UsuarioService {
  private baseUrl: string = "";
  private endpoint: string = "usuario/";

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.baseUrl = baseUrl;
  }

  listar() {
    return this.http.get(this.baseUrl + this.endpoint)
  }

  getById(id: number) {
    return this.http.get(this.baseUrl + this.endpoint + id);
  }

  deletar(id: number) {
    return this.http.delete(this.baseUrl + this.endpoint + id);
  }

  criar(body: UsuarioModel) {
    return this.http.post(this.baseUrl + this.endpoint, body);
  }

  atualizar(body: UsuarioModel) {
    return this.http.put(this.baseUrl + this.endpoint, body);
  }

  enviarHistorico(usuarioId: number, fileToUpload: File) {
    const formData: FormData = new FormData();
    formData.append('historico', fileToUpload, fileToUpload.name);
    return this.http.post(this.baseUrl + this.endpoint + 'historicoEscolar/' + usuarioId, formData)
  }

  baixarHistorico(usuarioId: number) {
    return this.http.get(this.baseUrl + this.endpoint + 'historicoEscolar/' + usuarioId, { responseType: 'blob' });
  }
}
