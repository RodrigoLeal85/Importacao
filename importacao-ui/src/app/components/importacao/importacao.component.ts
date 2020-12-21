import { Component, OnInit, ViewChild } from '@angular/core';
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
  displayedColumns: any[] = ['idImportacao', 'dataCadastro', 'numeroItems', 'menorDataEntrega', 'valorTotalImportacao'];

  constructor(private importacaoService: ImportacaoService) { }

  ngOnInit(): void {
    this.importacaoService.getImportacao().subscribe(x => this.importacoes = x);
  }
  
  importarArquivo(){
    const formData : FormData = new FormData();
    let file: File = this.fileInput.nativeElement.files[0];    
    formData.append('arquivoImportacao', file, file.name);
    this.importacaoService.insertImportacaoExcel(formData).subscribe();
  }

}
