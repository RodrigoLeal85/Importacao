import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ImportacaoDetalheComponent } from './importacao-detalhe.component';

describe('ImportacaoDetalheComponent', () => {
  let component: ImportacaoDetalheComponent;
  let fixture: ComponentFixture<ImportacaoDetalheComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ImportacaoDetalheComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ImportacaoDetalheComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
