import { error } from '@angular/compiler/src/util';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { flatMap } from 'rxjs';
import { EscolaridadeModel } from '../../interfaces/escolaridade-model';
import { UsuarioModel } from '../../interfaces/usuario-model';
import { EscolaridadeService } from '../../services/escolaridade.service';
import { UsuarioService } from '../../services/usuario.service';

@Component({
  selector: 'app-usuario-detail',
  templateUrl: './usuario-detail.component.html',
  styleUrls: ['./usuario-detail.component.css']
})
export class UsuarioDetailComponent implements OnInit {
  public action: string = "";
  public id: number = 0;
  public usuario: UsuarioModel = {
    id: 0,
    nome: '',
    sobrenome: '',
    email: '',
    dataNascimento: new Date(),
    escolaridade: ''
  };
  public escolaridades: EscolaridadeModel[] = [];
  public file: File | null = null;

  constructor(private route: ActivatedRoute, private router: Router, private usuarioService: UsuarioService, private escolaridadeService: EscolaridadeService) { }

  ngOnInit(): void {
    var param = this.route.snapshot.paramMap.get('action');
    this.action = param != null ? param : "";
    param = this.route.snapshot.paramMap.get('id');
    this.id = Number(param);
    if (this.action != 'novo' && this.action != 'editar') {
      alert("Esta ação não é permitida para usuario.")
      this.router.navigate(['/usuarios']);
    } else if (this.action == 'editar') {
      this.usuarioService.getById(this.id)
        .subscribe(result => {
          this.usuario = result as UsuarioModel;
        }, erro => {
          alert(erro.error);
        })
    }

    this.escolaridadeService.listar()
      .subscribe(result => {
        this.escolaridades = result as EscolaridadeModel[];
      }, erro => {
        alert(erro.error);
      })
    
  }

  onChange(event : any) {
    this.file = event.target.files[0];
  }

  save() {
    if (this.usuario.id != 0) {
      this.usuarioService.atualizar(this.usuario)
        .subscribe(result => {
          alert("Usuário atualizado com sucesso!");
          this.enviarArquivo(this.usuario.id);
          
        }, erro => {
          alert(erro.error);
        });
    } else {
      this.usuarioService.criar(this.usuario)
        .subscribe(result => {
          alert("Usuário atualizado com sucesso!");
          this.enviarArquivo(result as number);
        },erro => {
          alert(erro.error);
        });
    }
  }

  enviarArquivo(id: number) {
    this.usuarioService.enviarHistorico(id, <File>this.file)
      .subscribe(result => {
        this.router.navigate(["/usuarios"]);
      }, erro => {
        alert(erro.error);
      });
  }

}
