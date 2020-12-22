import { Injectable } from "@angular/core";
import { HttpClient, HttpHeaders, HttpErrorResponse } from '@angular/common/http';
import { ImportacaoViewModel } from "../models/importacaoViewModel";
import { Observable } from "rxjs";
import { map } from 'rxjs/operators';
import { catchError } from 'rxjs/operators';
import { throwError } from 'rxjs';
import { environment } from "src/environments/environment";

@Injectable({
    providedIn: 'root'
})
export class ImportacaoService {
    url = environment.apiUrl;
    constructor(private http: HttpClient){}

    insertImportacaoExcel(formData: FormData) {
        let headers = new HttpHeaders();
        headers.append('Content-Dispose', 'multipart/form-data');
        
        const httpOptions = { headers: headers };
        return this.http.post(this.url + '/importarExcel', formData, httpOptions).pipe(
            catchError(this.handleError)
            );
    }

    getImportacao() : Observable<ImportacaoViewModel[]>{
        return this.http.get(this.url).pipe(
            map((response:any) => {return response;}),
            catchError(this.handleError)
        );
    }

    getImportacaoPorID(id: number) : Observable<ImportacaoViewModel> {
        return this.http.get(this.url + '/' + id).pipe(
            map((response:any) => {return response;}),
            catchError(this.handleError)
        );
    }

    private handleError(error: HttpErrorResponse) {        
        if(error.status == 400){
            return throwError(
                'Importação não realizada. Por favor, corrigir os erros abaixo: \n ' + error.error);
        }else if(error.status == 404){
            return throwError(
                'Registro não encontrado.');
        }
        return throwError('Algum erro aconteceu. Verifique a mensagem abaixo: \n' + error.message);            
      }

}