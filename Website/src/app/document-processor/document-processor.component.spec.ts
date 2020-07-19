import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DocumentProcessorComponent } from './document-processor.component';

describe('DocumentProcessorComponent', () => {
  let component: DocumentProcessorComponent;
  let fixture: ComponentFixture<DocumentProcessorComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DocumentProcessorComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DocumentProcessorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
