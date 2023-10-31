import { Injectable } from '@angular/core';
import { Notyf } from 'notyf';

@Injectable({
    providedIn: 'root'
})
export class NotyfyService {
    notyf: any = new Notyf({
        position: {
            x: 'right',
            y: 'top',
        },
    });
    constructor() { }

    showSuccess(message: string) {
        this.notyf.success(message);
    }

    showError(message: string) {
        this.notyf.error(message);
    }

    showSuccessDismissable(message: string) {
        this.notyf
            .success({
                message: message,
                dismissible: true
            });
    }

    showErrorDismissable(message: string) {
        this.notyf
            .error({
                message: message,
                dismissible: true
            });
    }


}

