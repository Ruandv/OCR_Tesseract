import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ModalService } from './services/modal.service';
import { BrowserModule } from "@angular/platform-browser";
import { NgModule } from "@angular/core";
import { AppComponent } from "./app.component";
import { DocumentViewerComponent } from './document-viewer/document-viewer.component';
import { HttpModule } from "@angular/http";
import { ModalComponent } from './directives/modal.directive';
import { AngularFontAwesomeModule } from 'angular-font-awesome';

@NgModule({
  declarations: [AppComponent, DocumentViewerComponent, ModalComponent],
  imports: [BrowserModule, ReactiveFormsModule, HttpModule, FormsModule, AngularFontAwesomeModule],
  providers: [ModalService],
  bootstrap: [AppComponent]
})
export class AppModule { }
