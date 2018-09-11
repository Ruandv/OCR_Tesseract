import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { XmlParserComponent } from './xml-parser.component';

describe('XmlParserComponent', () => {
  let component: XmlParserComponent;
  let fixture: ComponentFixture<XmlParserComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ XmlParserComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(XmlParserComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
