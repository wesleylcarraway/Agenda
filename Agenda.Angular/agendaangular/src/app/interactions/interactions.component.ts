import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { lastValueFrom } from 'rxjs';
import { Interaction } from '../shared/entities/interaction';
import { BaseError } from '../shared/http-service/base-error';
import { InteractionsService } from '../shared/interactions-service/interactions.service';
import { apiErrorHandler } from '../shared/utils/api-error-handler';
import { jsPDF } from "jspdf";

@Component({
  selector: 'app-interactions',
  templateUrl: './interactions.component.html',
  styleUrls: ['./interactions.component.scss']
})
export class InteractionsComponent implements OnInit {
  data!: Interaction[];
  doc: jsPDF = new jsPDF();

  constructor(
    private interactionsService: InteractionsService,
    private snackBar: MatSnackBar,
    ) {
    }

  async ngOnInit(): Promise<void> {
    await this.getDataAsync();
  }

  generatePdf(): void {
    this.doc.setFontSize(10);
    this.doc.setFont("helvetica", "bold");

    this.doc.text("id", 10, 10);
    this.doc.text("message", 20, 10);
    this.doc.text("userId", 50, 10);
    this.doc.text("interactionTypeId", 80, 10);
    this.doc.text("createdAt", 130, 10);

    for(let i = 0; i < this.data.length; i++) {
      this.doc.setFont("helvetica", "normal");
      this.doc.text(this.data[i].id.toString(), 10, 20 + (i * 10));
      this.doc.text(this.data[i].message.toString(), 20, 20 + (i * 10));
      this.doc.text(this.data[i].userId.toString(), 50, 20 + (i * 10));
      this.doc.text(this.data[i].interactionTypeId.toString(), 80, 20 + (i * 10));
      this.doc.text(this.data[i].createdAt.toString(), 130, 20 + (i * 10));
    }

    this.doc.save("interactions.pdf");
  }

  async getDataAsync(): Promise<void> {
    try {
      this.data = await lastValueFrom(this.interactionsService.getInteractionsAsync());
    } catch ({ error }) {
      apiErrorHandler(this.snackBar, error as BaseError)
    }
  }
}
