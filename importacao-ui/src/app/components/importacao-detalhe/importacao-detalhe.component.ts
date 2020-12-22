import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';
import { ImportacaoViewModel } from 'src/app/models/importacaoViewModel';
import { ImportacaoService } from 'src/app/services/importacao.service';

@Component({
  selector: 'app-importacao-detalhe',
  templateUrl: './importacao-detalhe.component.html',
  styleUrls: ['./importacao-detalhe.component.scss']
})
export class ImportacaoDetalheComponent implements OnInit {
  displayedColumns: any[] = ['idImportacaoItem', 'dataEntrega', 'descricao', 'quantidade', 'valorUnitario', 'valorTotal'];
  importacao: ImportacaoViewModel = new ImportacaoViewModel();
  public error: any
  isLoading = true;

  constructor(private route: ActivatedRoute, private location: Location, private importacaoService: ImportacaoService) { }

  ngOnInit(): void {
    this.route.params.subscribe(p => {
      let idImportacao = p['id'];
      this.importacaoService.getImportacaoPorID(idImportacao).subscribe(x => { 
        this.isLoading = false;
        this.importacao = x;
      }, error => {
        this.isLoading = false;
        this.error = error;
      });
    });
  }

}
