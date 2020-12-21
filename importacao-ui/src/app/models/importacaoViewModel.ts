import { ImportacaoItemViewModel } from "./importacaoItemViewModel"

export class ImportacaoViewModel {
    idImportacao!: number
    dataCadastro!: Date
    numeroItems!: number
    menorDataEntrega!: Date
    valorTotalImportacao!: number

    importacaoItems!: ImportacaoItemViewModel[]
}