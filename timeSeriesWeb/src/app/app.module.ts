import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BlockUIModule } from 'ng-block-ui';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { OwlDateTimeModule, OwlNativeDateTimeModule } from '@danielmoncada/angular-datetime-picker';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HighchartsChartModule } from 'highcharts-angular'
import { DashboardSpComponent } from './dashboardsp/dashboardsp.component';
@NgModule({
    declarations: [
        AppComponent
        , DashboardComponent
        , DashboardSpComponent
    ],
    imports: [
        BrowserModule
        , FormsModule
        , ReactiveFormsModule
        , AppRoutingModule
        , HttpClientModule
        , BlockUIModule.forRoot()
        , BrowserAnimationsModule
        , OwlDateTimeModule
        , OwlNativeDateTimeModule
        , HighchartsChartModule
    ],
    providers: [],
    bootstrap: [AppComponent]
})
export class AppModule { }
