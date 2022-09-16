import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { PageEvent } from '@angular/material/paginator';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { lastValueFrom } from 'rxjs';
import { AgendaService } from '../shared/agenda-service/agenda.service';
import { ConfirmModalDialogConfig } from '../shared/components/confirm-modal-dialog/confirm-modal-dialog-config';
import { ConfirmModalDialogService } from '../shared/components/confirm-modal-dialog/confirm-modal-dialog.service';
import { LoadingModalConfig } from '../shared/components/loading-modal/loading-modal-config';
import { LoadingModalService } from '../shared/components/loading-modal/loading-modal.service';
import { SearchingForm } from '../shared/components/searching/searching-form';
import { SearchingInputConfig } from '../shared/components/searching/searching-input-config';
import { Contact } from '../shared/entities/contact';
import { BaseError } from '../shared/http-service/base-error';
import { BaseParams } from '../shared/http-service/base-params';
import { PaginationResponse } from '../shared/http-service/pagination-response';
import { apiErrorHandler } from '../shared/utils/api-error-handler';

@Component({
  selector: 'app-agenda',
  templateUrl: './agenda.component.html',
  styleUrls: ['./agenda.component.scss']
})
export class AgendaComponent implements OnInit{
  data!: PaginationResponse<Contact>;
  searchingConfig =  {
    searchAction: (query) => this.searchAsync(query),
    params: [
      { label: "Name", name: "name" },
      { label: "DDD", name: "ddd" },
      { label: "Number", name: "number" },
    ]
  } as SearchingInputConfig;

  constructor(
    private agendaService: AgendaService,
    private snackBar: MatSnackBar,
    private confirmModalDialogService: ConfirmModalDialogService,
    private cdRef: ChangeDetectorRef,
    private router: Router,
  ) { }

  async ngOnInit(): Promise<void> {
    await this.refreshTableAsync();
  }

  async getDataAsync(params = new BaseParams()): Promise<void> {
    try {
      this.data = await lastValueFrom(this.agendaService.getAsync(params));
    } catch ({ error }) {
      apiErrorHandler(this.snackBar, error as BaseError)
    }
  }

  onAdd() {
    this.router.navigate(['/dashboard/agenda/form/0'])
  }

  onEdit(id?: number) {
    this.router.navigate(['/dashboard/agenda/form', id])
  }

  async deleteContactAsync(id: number): Promise<void> {
    const config = {
      title: 'Confirm exclusion',
      message: 'Do you really wish to delete this contact?',
    } as ConfirmModalDialogConfig;
    this.confirmModalDialogService.open(config);
    this.confirmModalDialogService.closed.subscribe(async (result) => {
      if (result) {
        await lastValueFrom(this.agendaService.deleteAsync(id));
        await this.refreshTableAsync();
      }
    });
  }

  async refreshTableAsync(): Promise<void> {
    await this.getDataAsync();
    this.cdRef.detectChanges();
  }

  async changePageAsync(event: PageEvent): Promise<void> {
    const params = {
      take: event.pageSize,
      skip: event.pageIndex * event.pageSize,
    } as BaseParams;
    await this.getDataAsync(params);
  }

  async searchAsync(query: SearchingForm): Promise<void> {
    const params = {
      [query.field]: query.value
    } as BaseParams;
    await this.getDataAsync(params);
    this.cdRef.detectChanges();
  }
}
