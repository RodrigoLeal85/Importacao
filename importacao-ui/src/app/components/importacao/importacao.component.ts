import { Component, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { ImportacaoViewModel } from 'src/app/models/importacaoViewModel';
import { ImportacaoService } from 'src/app/services/importacao.service';

@Component({
  selector: 'app-importacao',
  templateUrl: './importacao.component.html',
  styleUrls: ['./importacao.component.scss']
})
export class ImportacaoComponent implements OnInit {
  @ViewChild('fileInput') fileInput : any;
  importacoes!: ImportacaoViewModel[]
  public error: any
  isLoading = true;
  displayedColumns: any[] = ['idImportacao', 'dataCadastro', 'numeroItems', 'menorDataEntrega', 'valorTotalImportacao'];

  constructor(private router: Router, private importacaoService: ImportacaoService) { }

  ngOnInit(): void {
    this.importacaoService.getImportacao().subscribe(x => {
      this.isLoading = false;
      this.importacoes = x;      
    }, error => this.isLoading = false);
  }
  
  importarArquivo(){
    const formData : FormData = new FormData();
    let file: File = this.fileInput.nativeElement.files[0];    
    formData.append('arquivoImportacao', file, file.name);
    this.importacaoService.insertImportacaoExcel(formData).subscribe(r => {
          this.router.navigate(['/importacao/' +  (r as ImportacaoViewModel).idImportacao]);
    }, error => {
      this.error = error;
    });
  }

}
