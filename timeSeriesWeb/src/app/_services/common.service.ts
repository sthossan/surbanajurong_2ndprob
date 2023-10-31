import { Injectable, ErrorHandler } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { BehaviorSubject, Observable, of } from 'rxjs';
import { delay, map } from 'rxjs/operators';
import { environment } from '../../environments/environment';
import { Router } from '@angular/router';

@Injectable({ providedIn: 'root' })
export class CommonService {
    constructor(
        private http: HttpClient
        , private router: Router
    ) { }


    sortList(list: [], fieldName: string): [] {
        list = list?.sort((a, b) => { return <any>new Date(b[fieldName]) - <any>new Date(a[fieldName]); });
        return list;
    }

    sortListByColumn(list: [], fieldName: string, order: string): [] {
        if (order.toLowerCase() == 'desc')
            list = list?.sort((a, b) => (<any>(b[fieldName])).localeCompare(a[fieldName]));
        else
            list = list?.sort((a, b) => (<any>(a[fieldName])).localeCompare(b[fieldName]));
        return list;
    }

    getList(url: string) {
        return this.http.get<any>(environment.apiUrl + url).pipe(
            map(res => {
                return res;
            })
        );
    }

    async getAsync(url: string) {
        const response = this.http.get<any>(environment.apiUrl + url).toPromise();
        return response;
    }

    postWithParams(url: string) {
        return this.http.post<any>(environment.apiUrl + url, null).pipe(
            map(res => {
                return res;
            })
        );
    }

    async postWithParamsAsync(url: string) {
        const response = await this.http.post<any>(environment.apiUrl + url, null).toPromise();
        return response;
    }


    postWithFormData(url: string, formData: any) {
        return this.http.post<any>(environment.apiUrl + url, formData).pipe(
            map(res => {
                return res;
            })
        );
    }
}
