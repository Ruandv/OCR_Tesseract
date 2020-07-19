import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ModalService } from './services/modal.service';
import { BrowserModule } from "@angular/platform-browser";
import { NgModule } from "@angular/core";
import { AppComponent } from "./app.component";
import { DocumentViewerComponent } from './document-viewer/document-viewer.component';
import { ModalComponent } from './directives/modal.directive';
import { AngularFontAwesomeModule } from 'angular-font-awesome';
import { DocumentProcessorComponent } from './document-processor/document-processor.component';
import { HttpClientModule } from '@angular/common/http';

@NgModule({
  declarations: [AppComponent, DocumentViewerComponent, ModalComponent, DocumentProcessorComponent],
  imports: [BrowserModule, ReactiveFormsModule,  HttpClientModule, FormsModule, AngularFontAwesomeModule],
  providers: [ModalService],
  bootstrap: [AppComponent]
})
export class AppModule { }
