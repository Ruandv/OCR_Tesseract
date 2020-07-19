import { ModalEmailInfo } from './../../Models/Region';
import { OcrTemplateService } from './../services/template.service';
import {
  Component,
  Input,
  ViewChild,
  ElementRef,
  AfterViewInit,
  OnChanges,
  SimpleChanges,
  Output,
  EventEmitter,
} from "@angular/core";
import { Region, Point, ModalInfo } from "src/Models/Region";
import { ModalService } from '../services/modal.service';
export enum Keys {
  Enter = 13,
  Delete = 46,
  Backspace = 8,
  LeftArrow = 37,
  RightArrow = 39,
  UpArrow = 38,
  DownArrow = 40
}
@Component({
  selector: "app-document-viewer",
  templateUrl: "./document-viewer.component.html",
  styleUrls: ["./document-viewer.component.css"],
})
export class DocumentViewerComponent implements AfterViewInit, OnChanges {
  @Input()
  get source(): string {
    return this._source;
  }

  set source(value: string) {
    if (value !== this._source) {
      this._source = value;

      this.myImage.src = this._source;

      this.myImage.onload = () => {
        this.canvasCopy = document.createElement("canvas");
        this.canvasCopy.setAttribute("id", "canvasCopy1");
        document.getElementById("mainDoc").appendChild(this.canvasCopy);
        this.ratio = 1;
        if (this.myImage.width > this.maxWidth) {
          this.ratio = this.maxWidth / this.myImage.width;
        } else if (this.myImage.height > this.maxHeight) {
          this.ratio = this.maxHeight / this.myImage.height;
        }

        this.canvasCopy.width = this.myImage.width;
        this.canvasCopy.height = this.myImage.height;

        const copyContext = this.canvasCopy.getContext("2d");
        copyContext.drawImage(this.myImage, 0, 0);

        this.canvas.nativeElement.width = this.myImage.width * this.ratio;
        this.canvas.nativeElement.height = this.myImage.height * this.ratio;

        this.cx.drawImage(
          this.myImage,
          0,
          0,
          this.canvasCopy.width,
          this.canvasCopy.height,
          0,
          0,
          this.canvas.nativeElement.width,
          this.canvas.nativeElement.height
        );
        this.drawRegions();
      };
    }
  }
  @Input()
  public regions: Region[];

  @Input()
  public fileName: string;

  @ViewChild("canvas")
  public canvas: ElementRef;

  @Output()
  templateSaved = new EventEmitter();

  private activeRegion: Region;
  public regionType: string;

  public width = 600;
  public height = 800;

  private myImage = new Image();
  private cx: CanvasRenderingContext2D;
  private _source;
  private canvasCopy: any;
  private ratio: any;
  private maxWidth = 600;
  private maxHeight = 800;

  modalInfo: any;
  emailModalInfo = new ModalEmailInfo();
  MyX: any;
  MyY: any;

  constructor(private service: OcrTemplateService, public modalService: ModalService) {
    this.modalInfo = new ModalInfo();
  }
  ngOnChanges(changes: SimpleChanges): void {
    console.log("changes detected");
  }

  ngAfterViewInit(): void {
    const canvasEl: HTMLCanvasElement = this.canvas.nativeElement;
    this.cx = canvasEl.getContext("2d");
    this.regionType = 'Identifier';
    // set the width and height
    canvasEl.width = this.width;
    canvasEl.height = this.height;

    // set some default properties about the line
    this.cx.lineWidth = 3;
    this.cx.lineCap = "round";
    this.cx.strokeStyle = "#000";

  }

  drawOnCanvas(topLeftPos: Point, width: number, heigh: number, strokeColor: string) {
    if (topLeftPos.x < 0 || topLeftPos.y < 0) {
      return;
    }

    // incase the context is not set
    if (!this.cx) {
      return;
    }

    // var copyContext = this.canvasCopy.getContext("2d");
    // copyContext.beginPath();
    // copyContext.rect(
    //   this.myImage.width * (topLeftPos.x / this.maxWidth * 100) / 100,
    //   this.myImage.width * (topLeftPos.y / this.maxWidth * 100) / 100,
    //   (this.myImage.width * (bottomRightPos.x / this.maxWidth * 100) / 100)
    //    - (this.myImage.width * (topLeftPos.x / this.maxWidth * 100) / 100),
    //   (this.myImage.width * (bottomRightPos.y / this.maxWidth * 100) / 100)
    //    - (this.myImage.width * (topLeftPos.y / this.maxWidth * 100) / 100)
    // );
    // copyContext.stroke();

    // start our drawing path
    this.cx.beginPath();
    this.cx.strokeStyle = strokeColor;
    this.cx.rect(
      topLeftPos.x * (this.canvas.nativeElement.width / this.maxWidth),
      topLeftPos.y * (this.canvas.nativeElement.width / this.maxWidth),
      width * (this.canvas.nativeElement.width / this.maxWidth),
      heigh * (this.canvas.nativeElement.width / this.maxWidth)
    );

    // strokes the current path with the styles we set earlier
    this.cx.stroke();
  }

  zoomIn(): void {
    this.ratio = this.ratio * 1.1;
    this.reDrawImage(undefined);
  }

  zoomOut(): void {
    this.ratio =
      Math.round(
        (this.ratio + (this.ratio - 10) / 100 + Number.EPSILON) * 100
      ) / 100;
    this.reDrawImage(undefined);
  }
  deleteTemplate() {
    this.service.deleteTemplate(this.fileName).subscribe(x => {
      this.templateSaved.emit('Removed');
    });
  }
  saveTemplate() {
    this.service.saveTemplate(this.regions, this.fileName).subscribe(x => {
      this.templateSaved.emit('Saved');
    });
  }
  // ocrRecord(region: Region, index) {
  //   const data: any = {
  //     description: region.description,
  //     topLeft: {
  //       x: Math.round(this.myImage.width * (region.topLeft.x / this.maxWidth * 100) / 100),
  //       y: Math.round(this.myImage.width * (region.topLeft.y / this.maxWidth * 100) / 100)
  //     },
  //     width: Math.round((this.myImage.width * (region.bottomRight.x / this.maxWidth * 100) / 100)
  //       - this.myImage.width * (region.topLeft.x / this.maxWidth * 100) / 100),
  //     height: Math.round((this.myImage.width * (region.bottomRight.y / this.maxWidth * 100) / 100)
  //       - this.myImage.width * (region.topLeft.y / this.maxWidth * 100) / 100)
  //   };
  //   // this.service.ocrDocument(this.fileName, [data]).subscribe(x => {
  //   //   this.regions[index].description = x._body;
  //   //   console.log(x._body);
  //   // });
  // }

  ocrData() {
    // const re = [];
    // this.regions.forEach(region => {
    //   re.push({
    //     description: region.description,
    //     topLeft: {
    //       x: Math.round(this.myImage.width * (region.topLeft.x / this.maxWidth * 100) / 100),
    //       y: Math.round(this.myImage.width * (region.topLeft.y / this.maxWidth * 100) / 100)
    //     },
    //     width: Math.round((this.myImage.width * (region.bottomRight.x / this.maxWidth * 100) / 100)
    //       - this.myImage.width * (region.topLeft.x / this.maxWidth * 100) / 100),
    //     height: Math.round((this.myImage.width * (region.bottomRight.y / this.maxWidth * 100) / 100)
    //       - this.myImage.width * (region.topLeft.y / this.maxWidth * 100) / 100)
    //   });
    // });
    // // this.service.ocrDocument(this.fileName, re).subscribe(x => {
    // //   this.regions[0].description = x._body;
    // //   console.log(x._body);
    // // });
  }

  clear(): void {
    this.cx.clearRect(0, 0, this.cx.canvas.width, this.cx.canvas.height);
    this.regions = [];
    this.reDrawImage(undefined);
  }

  mouseUp(event) {
    this.activeRegion.width = Math.abs(this.MyX - this.activeRegion.topLeft.x);
    this.activeRegion.height = Math.abs(this.MyY - this.activeRegion.topLeft.y);
    this.regions.push(this.activeRegion);
    this.drawRegions(this.regions.length);
    // this.ocrRecord(this.newRegion, this.regions.length - 1);

  }

  mouseDown(event) {
    if (!this.isInRegion()) {
      this.activeRegion = new Region();
      this.activeRegion.description = 'Some D';
      this.activeRegion.index = this.regions.length;
      this.activeRegion.regionType = this.regionType;
      this.activeRegion.topLeft = {
        x: this.MyX,
        y: this.MyY,
      };
    }
  }

  isInRegion(): boolean {
    const res = this.regions.find(x => x.topLeft.x < this.MyX && (x.topLeft.x + x.width) > this.MyX &&
      x.topLeft.y < this.MyY && (x.topLeft.y + x.height) > this.MyY);
    this.activeRegion = res;

    return this.activeRegion !== undefined;
  }

  mouseMove(event) {
    const rect = this.canvas.nativeElement.getBoundingClientRect();
    this.MyX =
      (event.clientX - rect.x) /
      (this.canvas.nativeElement.width / this.maxWidth);
    this.MyY =
      (event.clientY - rect.y) /
      (this.canvas.nativeElement.width / this.maxWidth);
  }

  changeType(type: string) {
    console.log("Type set to " + type);
    this.regionType = type;
    this.reDrawImage(-1);
  }

  isActiveRegion(index): boolean {
    if (!this.activeRegion) {
      return false;
    }
    return this.activeRegion.index === index;
  }
  showEmailModal() {
    this.modalInfo = new ModalEmailInfo();
    this.modalInfo.header = "Sending Email";
    this.modalInfo.to = 'Ruan.dv@k2.com';
    this.modalInfo.subject = "Hello World";
    this.modalInfo.message = "This is me... :-)";

    this.modalService.open('custom-email-modal');
  }

  setDescription(index) {
    this.modalInfo = new ModalInfo();
    this.modalInfo.header = "Edit Description";
    this.modalInfo.index = index;
    this.modalInfo.textBinding = this.regions[index].description;
    this.modalService.open('custom-modal-1');
  }

  closeModal(id: string) {
    this.regions[this.modalInfo.index].description = this.modalInfo.textBinding;
    this.modalService.close(id);
  }

  regionKeyDown(event, index): void {
    switch (event.keyCode) {
      case Keys.Delete:
        this.regions.splice(index, 1);
        this.reDrawImage(undefined);
        event.preventDefault();
        break;
      case Keys.UpArrow:
        this.activeRegion.topLeft.y -= 1;
        // this.activeRegion.bottomRight.y -= 1;
        event.preventDefault();
        break;
      case Keys.DownArrow:
        this.activeRegion.topLeft.y += 1;
        // this.activeRegion.bottomRight.y += 1;
        event.preventDefault();
        break;
      case Keys.LeftArrow:
        this.activeRegion.topLeft.x -= 1;
        // this.activeRegion.bottomRight.x -= 1;
        event.preventDefault();
        break;
      case Keys.RightArrow:
        this.activeRegion.topLeft.x += 1;
        // this.activeRegion.bottomRight.x += 1;
        event.preventDefault();
        break;
    }
    this.reDrawImage(this.activeRegion.index);
    console.log(event.keyCode);
  }

  setActive(index) {
    console.log("index " + index);
    this.activeRegion = this.regions[index];
    this.reDrawImage(index);
  }

  reDrawImage(activeRegion: number) {
    const copyContext = this.canvasCopy.getContext("2d");
    copyContext.drawImage(this.myImage, 0, 0);

    this.canvas.nativeElement.width = this.myImage.width * this.ratio;
    this.canvas.nativeElement.height = this.myImage.height * this.ratio;

    this.cx.drawImage(this.myImage, 0, 0, this.canvasCopy.width, this.canvasCopy.height,
      0, 0, this.canvas.nativeElement.width, this.canvas.nativeElement.height);
    this.drawRegions(activeRegion);
  }

  drawRegions(activeRegion?: number) {
    this.regions.forEach((element, k) => {
      if (element.regionType === this.regionType) {
        if (k === activeRegion) {
          this.drawOnCanvas(element.topLeft, element.width, element.height, "red");
        } else {
          this.drawOnCanvas(element.topLeft, element.width, element.height, "black");
        }
      }
    });
  }
}
