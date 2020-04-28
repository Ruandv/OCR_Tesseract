import { Headers } from '@angular/http';
import { DocumentService } from './../services/document.service';
import {
  Component,
  Input,
  ViewChild,
  ElementRef,
  AfterViewInit,
} from "@angular/core";
import { Region, Point, ModalInfo } from "src/Models/Region";
import { ModalService } from '../services/modal.service';

@Component({
  selector: "app-document-viewer",
  templateUrl: "./document-viewer.component.html",
  styleUrls: ["./document-viewer.component.css"],
})
export class DocumentViewerComponent implements AfterViewInit {
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

        var copyContext = this.canvasCopy.getContext("2d");
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
      };
    }
  }
  @Input()
  public fileName: string;
  @Input()
  public width = 600;
  @Input()
  public height = 800;

  @ViewChild("canvas")
  public canvas: ElementRef;

  @ViewChild("img")
  public imageCan: any;

  private modalInfo = new ModalInfo();
  private myImage = new Image();
  private cx: CanvasRenderingContext2D;
  private _source;
  private canvasCopy: any;
  private ratio: any;
  private maxWidth = 600;
  private maxHeight = 800;
  public regions: Region[];

  private endPos: Point;
  private startPos: Point;

  private MyX: any;
  private MyY: any;

  constructor(private service: DocumentService, private modalService: ModalService) {
    this.regions = [];
    // this.modalInfo.header = "Edit Description";
    // this.modalInfo.textBinding = "SDF";
  }

  ngAfterViewInit(): void {
    const canvasEl: HTMLCanvasElement = this.canvas.nativeElement;
    this.cx = canvasEl.getContext("2d");

    // set the width and height
    canvasEl.width = this.width;
    canvasEl.height = this.height;

    // set some default properties about the line
    this.cx.lineWidth = 3;
    this.cx.lineCap = "round";
    this.cx.strokeStyle = "#000";
  }

  drawOnCanvas(topLeftPos: Point, bottomRightPos: Point, strokeColor: string) {
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
    //copyContext.stroke();

    // start our drawing path
    this.cx.beginPath();
    this.cx.strokeStyle = strokeColor;
    this.cx.rect(
      topLeftPos.x * (this.canvas.nativeElement.width / this.maxWidth),
      topLeftPos.y * (this.canvas.nativeElement.width / this.maxWidth),
      bottomRightPos.x * (this.canvas.nativeElement.width / this.maxWidth) -
      topLeftPos.x * (this.canvas.nativeElement.width / this.maxWidth),
      bottomRightPos.y * (this.canvas.nativeElement.width / this.maxWidth) -
      topLeftPos.y * (this.canvas.nativeElement.width / this.maxWidth)
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

  ocrRecord(region, index) {
    const data: any = {
      description: region.description,
      topLeft: {
        x: Math.round(this.myImage.width * (region.topLeft.x / this.maxWidth * 100) / 100),
        y: Math.round(this.myImage.width * (region.topLeft.y / this.maxWidth * 100) / 100)
      },
      width: Math.round((this.myImage.width * (region.bottomRight.x / this.maxWidth * 100) / 100)
        - this.myImage.width * (region.topLeft.x / this.maxWidth * 100) / 100),
      height: Math.round((this.myImage.width * (region.bottomRight.y / this.maxWidth * 100) / 100)
        - this.myImage.width * (region.topLeft.y / this.maxWidth * 100) / 100)
    };
    this.service.ocrDocument(this.fileName,[data]).subscribe(x => {
      this.regions[index].description = x._body;
      console.log(x._body);
    });
  }

  ocrData() {
    const re = [];
    this.regions.forEach(region => {
      re.push({
        description: region.description,
        topLeft: {
          x: Math.round(this.myImage.width * (region.topLeft.x / this.maxWidth * 100) / 100),
          y: Math.round(this.myImage.width * (region.topLeft.y / this.maxWidth * 100) / 100)
        },
        width: Math.round((this.myImage.width * (region.bottomRight.x / this.maxWidth * 100) / 100)
          - this.myImage.width * (region.topLeft.x / this.maxWidth * 100) / 100),
        height: Math.round((this.myImage.width * (region.bottomRight.y / this.maxWidth * 100) / 100)
          - this.myImage.width * (region.topLeft.y / this.maxWidth * 100) / 100)
      });
    });
    this.service.ocrDocument(this.fileName,re).subscribe(x => {
      alert("asdf");
    });
  }

  clear(): void {
    this.cx.clearRect(0, 0, this.cx.canvas.width, this.cx.canvas.height);
    this.regions = [];
    this.reDrawImage(undefined);
  }


  mouseUp(event) {
    this.endPos = {
      x: this.MyX,
      y: this.MyY,
    };
    const data = {
      description: `Region ${this.regions.length}`,
      topLeft: this.startPos,
      bottomRight: this.endPos,
    };
    this.regions.push(data);
    this.drawRegions(undefined);
    this.ocrRecord(data, this.regions.length - 1);
  }

  mouseDown(event) {
    this.startPos = {
      x: this.MyX,
      y: this.MyY,
    };
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

  setDescription(index) {
    this.modalInfo.header = "Edit Description";
    this.modalInfo.index = index;
    this.modalInfo.textBinding = this.regions[index].description;
    this.modalService.open('custom-modal-1');
  }

  closeModal(id: string) {
    this.regions[this.modalInfo.index].description = this.modalInfo.textBinding;
    this.modalService.close(id);
  }

  setActive(index) {
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

  drawRegions(activeRegion: number) {
    this.regions.forEach((element, k) => {
      if (k === activeRegion) {
        this.drawOnCanvas(element.topLeft, element.bottomRight, "red");
      } else {
        this.drawOnCanvas(element.topLeft, element.bottomRight, "black");
      }
    });
  }
}
