import { Injectable } from "@angular/core";
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { ImportacaoViewModel } from "../models/importacaoViewModel";
import { Observable } from "rxjs";
import { map } from 'rxjs/operators';

@Injectable({
    providedIn: 'root'
})
export class ImportacaoService {
    url = 'https://localhost:44338/importacao';
    constructor(private http: HttpClient){}

    insertImportacaoExcel(formData: FormData){
        let headers = new HttpHeaders();
        headers.append('Content-Dispose', 'multipart/form-data');
        
        const httpOptions = { headers: headers };
        console.log(formData);
        return this.http.post(this.url + '/importarExcel', formData, httpOptions)
    }

    getImportacao() : Observable<ImportacaoViewModel[]>{
        return this.http.get(this.url).pipe(
            map((response:any) => {return response;})
        );
    }

    getImportacaoPorID(id: number) : Observable<ImportacaoViewModel> {
        return this.http.get(this.url + '/' + id).pipe(
            map((response:any) => {return response;})
        );
    }

}