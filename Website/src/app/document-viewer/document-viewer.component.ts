import {
  Component,
  Input,
  ViewChild,
  ElementRef,
  AfterViewInit
} from "@angular/core";
import { fromEvent } from "rxjs/internal/observable/fromEvent";
import { Subscription } from "rxjs/internal/Subscription";

@Component({
  selector: "app-document-viewer",
  templateUrl: "./document-viewer.component.html",
  styleUrls: ["./document-viewer.component.css"]
})
export class DocumentViewerComponent implements AfterViewInit {
  @Input()
  source;
  @Input()
  public width = 400;
  @Input()
  public height = 400;

  @ViewChild("canvas")
  public canvas: ElementRef;
  private endPos: any;
  private startPos: any;
  private cx: CanvasRenderingContext2D;
  private points = [];
  private zoomCounter = 1;
  private point: { x: any; y: any; };
  constructor() {}

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

    // we'll implement this method to start capturing mouse events
    this.captureEvents(canvasEl);
  }

  private captureEvents(canvasEl: HTMLCanvasElement) {
    const down$ = fromEvent(canvasEl, "mousedown");
    const up$ = fromEvent(canvasEl, "mouseup");
    const move$ = fromEvent(canvasEl, "mousemove");
    const showPoint = ev => {
      this.point = { x: ev.layerX, y: ev.layerY };
    };
    const log = x => {
      const rect = canvasEl.getBoundingClientRect();

      console.log(x.type);
      if (x.type === "mouseup") {
        this.endPos = {
          x: x.layerX /this.zoomCounter,
          y: x.layerY/this.zoomCounter
        };
        this.points.push({ start: this.startPos, end: this.endPos });
        this.drawOnCanvasPoints();
      } else {
        this.startPos = {
          x: x.layerX/this.zoomCounter ,
          y: x.layerY/this.zoomCounter
        };
      }
    };
    down$.subscribe(log);
    up$.subscribe(log);
    move$.subscribe(showPoint);
  }

  drawOnCanvasPoints() {
    // incase the context is not set
    if (!this.cx) {
      return;
    }
    this.points.forEach(point => {
      this.cx.beginPath();

      // draws a line from the start pos until the current position
      this.cx.rect(
        point.start.x * this.zoomCounter,
        point.start.y * this.zoomCounter,
        point.end.x * this.zoomCounter - point.start.x * this.zoomCounter,
        point.end.y * this.zoomCounter - point.start.y * this.zoomCounter
      );

      // strokes the current path with the styles we set earlier
      this.cx.stroke();
    });
    // start our drawing path
  }

  drawOnCanvas(
    prevPos: { x: number; y: number },
    currentPos: { x: number; y: number }
  ) {
    // incase the context is not set
    if (!this.cx) {
      return;
    }

    // start our drawing path
    this.cx.beginPath();

    // draws a line from the start pos until the current position
    this.cx.rect(
      prevPos.x,
      prevPos.y,
      currentPos.x - prevPos.x,
      currentPos.y - prevPos.y
    );

    // strokes the current path with the styles we set earlier
    this.cx.stroke();
  }

  zoomIn(): void {
    this.zoomCounter += 1;
    this.cx.canvas.width += this.width;
    this.cx.canvas.height += this.height;
    const canvasEl: HTMLCanvasElement = this.canvas.nativeElement;
    this.cx = canvasEl.getContext("2d");

    // set the width and height
    canvasEl.width = this.cx.canvas.width;
    canvasEl.height = this.cx.canvas.height;
    this.drawOnCanvasPoints();
  }

  zoomOut(): void {
    this.zoomCounter -= 1;
    this.cx.canvas.width -= this.width;
    this.cx.canvas.height -= this.height;
    const canvasEl: HTMLCanvasElement = this.canvas.nativeElement;
    this.cx = canvasEl.getContext("2d");

    // set the width and height
    canvasEl.width = this.cx.canvas.width;
    canvasEl.height = this.cx.canvas.height;
    this.drawOnCanvasPoints();
  }

  clear(): void {
    this.cx.clearRect(0, 0, this.cx.canvas.width, this.cx.canvas.height);
    this.points = [];
  }
}
