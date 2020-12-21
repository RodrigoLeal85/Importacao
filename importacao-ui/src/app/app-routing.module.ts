import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ImportacaoDetalheComponent } from './components/importacao-detalhe/importacao-detalhe.component';
import { ImportacaoComponent } from './components/importacao/importacao.component';

const routes: Routes = [
  {path: '', component: ImportacaoComponent},
  {path: 'importacao/:id', component: ImportacaoDetalheComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
