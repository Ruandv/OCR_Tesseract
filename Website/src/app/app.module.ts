import { FormsModule } from '@angular/forms';
import { ModalService } from './services/modal.service';
import { BrowserModule } from "@angular/platform-browser";
import { NgModule } from "@angular/core";
import { AppComponent } from "./app.component";
import { DocumentViewerComponent } from './document-viewer/document-viewer.component';
import { HttpModule } from "@angular/http";
import { ModalComponent } from './directives/modal.directive';

@NgModule({
  declarations: [AppComponent, DocumentViewerComponent, ModalComponent],
  imports: [BrowserModule, HttpModule, FormsModule],
  providers: [ModalService],
  bootstrap: [AppComponent]
})
export class AppModule { }
