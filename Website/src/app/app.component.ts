import { OcrTemplateService } from './services/template.service';
import { Component, OnInit, ElementRef, ViewChild } from "@angular/core";
import { DocumentService } from "./services/document.service";
import { NgForm } from '@angular/forms';
import { Region } from 'src/Models/Region';

@Component({
  selector: "app-root",
  templateUrl: "./app.component.html",
  styleUrls: ["./app.component.css"]
})
export class AppComponent implements OnInit {
  data$: any;
  title = "Simple OCR";
  documents: any[] = [];
  imageSource = "";
  @ViewChild("frm")
  formRef: ElementRef;
  @ViewChild("documentInput")
  documentInputRef: ElementRef;
  @ViewChild("uploadStatus")
  uploadStatusRef: ElementRef;
  fileToUpload = 'Template File';
  templates: any;
  templateName: any;

  regions: Region[];
  showTemplate: boolean;
  constructor(private service: OcrTemplateService) { }

  ngOnInit(): void {
    this.uploadStatusRef.nativeElement.style.display = 'none';
    this.formRef.nativeElement.style.display = 'none';
    this.getTemplates();
  }
  private getTemplates() {
    this.service.getTemplates().subscribe((x: any) => {
      this.templates = x;
    });
  }
  uploadDocument(frm: NgForm): void {
    this.uploadStatusRef.nativeElement.style.display = 'block';
    this.service.uploadDocument(frm.value.TemplateName, this.documents).subscribe((x: any) => {
      this.imageSource = "data:image/png;base64," + x.file;
      this.title = x.fileName;
      this.templateName = this.title;
      this.uploadStatusRef.nativeElement.style.display = 'none';
      this.formRef.nativeElement.style.display = 'none';
    });
  }

  getTemplateImage(template) {
    this.service.getTemplate(template.templateDescription).subscribe((x: any) => {
      this.imageSource = "data:image/png;base64," + x;
      this.regions = template.identificationData as Region[];
      this.templateName = template.templateDescription;
    });
  }

  showNewTemplate(displayValue: string) {
    this.formRef.nativeElement.style.display = displayValue;
  }

  onTemplateSaved(): void {
    this.getTemplates();
  }
  onInput(event) {
    for (const file of event.target.files) {
      this.fileToUpload = file.name;
      this.documents.push({
        file: file,
        type: ""
      });
    }
  }

}
