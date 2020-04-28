import { Component, OnInit, ElementRef, ViewChild } from "@angular/core";
import { DocumentService } from "./services/document.service";

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
  @ViewChild("documentInput")
  documentInputRef: ElementRef;
  @ViewChild("uploadButton")
  uploadButtonRef: ElementRef;

  ngOnInit(): void {
    this.service.getDocuments().subscribe(x => {
      this.data$ = x;
      this.imageSource = "data:image/png;base64," + x[1];
    });
  }

  uploadDocument(): void {
    this.uploadButtonRef.nativeElement.className = 'disableButtonClass';
    this.service.uploadDocument(this.documents).subscribe(x => {
      this.imageSource = "data:image/png;base64," + JSON.parse(x.text()).file;
      this.title = JSON.parse(x.text()).fileName;
      this.uploadButtonRef.nativeElement.className = 'btn btn-sm btn-secondary';
    });
  }

  onInput(event) {
    for (const file of event.target.files) {
      this.documents.push({
        file: file,
        type: ""
      });
      this.uploadDocument();
    }
  }

  constructor(private service: DocumentService) { }
}
