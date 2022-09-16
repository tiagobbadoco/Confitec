import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UsuarioModel } from '../../interfaces/usuario-model';
import { UsuarioService } from '../../services/usuario.service';

@Component({
  selector: 'app-usuarios',
  templateUrl: './usuario-list.component.html',
  styleUrls: ['./usuario-list.component.css']
})
export class UsuarioListComponent implements OnInit {
  public usuarios: UsuarioModel[] = []

  constructor(private usuarioService: UsuarioService, private router: Router) { }

  ngOnInit(): void {
    this.listar();
  }

  listar() {
    this.usuarioService.listar()
      .subscribe(result => {
        this.usuarios = result as UsuarioModel[];
      })
  }

  adicionar() {
    this.router.navigate(["/usuario/novo"]);
  }

  editar(usuario: UsuarioModel) {
    this.router.navigate(["/usuario/editar/" + usuario.id]);
  }

  deletar(usuario: UsuarioModel) {
    if (confirm("Tem certeza que deseja excluir o usuario " + usuario.id + " ?")) {
      this.usuarioService.deletar(usuario.id).subscribe(result => {
        alert('Usuário excluído com sucesso!');
        this.listar();
      }, erro => {
        alert(erro.error);
      });
    }
  }

  baixarHistorico(usuario: UsuarioModel) {
    this.usuarioService.baixarHistorico(usuario.id)
      .subscribe(result => {
        this.downloadFile(result);
      }, erro => {
        alert("Não foi possível recuperar o arquivo solicitado");
      });
  }

  downloadFile(data: Blob) {
    const blob = new Blob([data], { type: data.type });
    const url = window.URL.createObjectURL(blob);
    window.open(url);
  }
}
