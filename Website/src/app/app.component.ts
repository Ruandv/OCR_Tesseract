import { Component, OnInit } from '@angular/core';
import { DocumentService } from './services/document.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  data$;
  title = 'Website';

  ngOnInit(): void {
    // this.service.getDocuments().subscribe(x=> {
    //   this.data$ =x;
    // });
  }

  constructor(private service: DocumentService) {}
}
