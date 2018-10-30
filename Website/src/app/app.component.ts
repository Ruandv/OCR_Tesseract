import { Component, OnInit, ElementRef, ViewChild } from "@angular/core";
import { DocumentService } from "./services/document.service";

@Component({
  selector: "app-root",
  templateUrl: "./app.component.html",
  styleUrls: ["./app.component.css"]
})
export class AppComponent implements OnInit {
  data$: any;
  title = "Website";
  documents: any[] = [];

  @ViewChild("documentInput")
  documentInputRef: ElementRef;

  ngOnInit(): void {
    this.service.getDocuments().subscribe(x => {
      this.data$ = x;
    });
  }

  uploadDocument(): void {
    this.service.uploadDocument(this.documents).subscribe(x => {
      alert("B");
    });
  }

  onInput(event) {
    for (const file of event.target.files) {
      this.documents.push({
        file: file,
        type: ""
      });
    }
  }

  constructor(private service: DocumentService) {}
}
