import { DatePipe } from '@angular/common';
import { Component, Input, OnInit } from '@angular/core';
import * as Highcharts from 'highcharts';
import { Options } from 'highcharts';
// import { Chart, Options, PointOptionsObject } from 'highcharts'
// import HC_map from "highcharts/modules/map";
// HC_map(Highcharts);

import { BlockUI, NgBlockUI } from 'ng-block-ui';
import { CommonService } from '../_services/common.service';
import { NotyfyService } from '../_services/notyfy.service';

@Component({
    selector: 'app-dashboard',
    templateUrl: './dashboard.component.html',
    styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {
    @BlockUI() blockUI!: NgBlockUI;

    buildingList = [];
    selectedBuilding: number = 0;

    objectList = [];
    selectedObject: number = 0;

    dataFieldList = [];
    selectedDataField: number = 0;

    readingList: any = [];

    //#region Date time picker
    min: Date | string = '';
    max: Date | string = '';
    selectDate: any = [];
    //#endregion Date time picker

    submitted = false;

    //#region Chart

    Highcharts: typeof Highcharts = Highcharts;
    // chartConstructor: string = 'chart';
    // chartOptions: any;
    // chartCallback: Highcharts.ChartCallbackFunction = function (chart) { }
    // updateFlag: boolean = true;
    // oneToOneFlag: boolean = true;
    // runOutsideAngular: boolean = true;

    //#endregion Chart

    constructor(
        private commonService: CommonService
        , public notyfy: NotyfyService
    ) { }

    ngOnInit(): void {
        this.selectDate[0] = new Date('2021/01/01 00:00:00');
        this.selectDate[1] = new Date('2021/01/02 23:59:59');
        this.selectedBuilding = 1;
        this.selectedObject = 1;
        this.selectedDataField = 1;

        this.getBuildingAsync();
        this.getObjectAsync();
        this.getDataFieldAsync();
        this.getReadingAsync();
    }

    async getBuildingAsync() {
        await this.commonService.getAsync('reading/getbuildingddl')
            .then(response => {
                this.buildingList = response; //this.commonService.sortList(response, 'timeStamp');
            });
    }

    async getObjectAsync() {
        await this.commonService.getAsync('reading/getobjectddl')
            .then(response => {
                this.objectList = response;
            });
    }

    async getDataFieldAsync() {
        await this.commonService.getAsync('reading/getDataFieldddl')
            .then(response => {
                this.dataFieldList = response;
            });
    }

    async getReadingAsync() {
        this.blockUI.start('Wait...');
        await this.commonService.getAsync('reading/getreadinglist?buildingId=' + this.selectedBuilding + '&objectId=' + this.selectedObject + '&datafieldId=' + this.selectedDataField + '&startDateTime=' + this.dateFormating(this.selectDate[0]) + '&endDateTime=' + this.dateFormating(this.selectDate[1]))
            .then(response => {
                // this.readingList = response;
                response.forEach((item: any) => {
                    let newArray = [];
                    let value_x = item.timestamp;
                    let value_y = item.value;
                    newArray.push(new Date(value_x), value_y)
                    this.readingList.push(newArray);
                });
                // if (response.length > 0)
                //     this.createChart();
                this.removeSeries();
                this.addSeries();
            });
    }

    search() {
        if (this.limitDaysRangeSelection())
            this.getReadingAsync();
    }

    //#region Chart

    chartRef: any;
    updateFlag: boolean = true;
    // createChart() { }
    // chartOptions: Highcharts.Options = {}
    chartOptions = {
        chart: {
            type: "spline",
            zoomType: 'x'
        },
        title: {
            text: "Time Series Data"
        },
        xAxis: {
            type: 'datetime',
            // minRange: 14 * 60, //14 * 24 * 3600000 // fourteen days
            maxZoom: 10 * 1000,
            // dateTimeLabelFormats: {
            //     // day: "%e-%b-%y",
            //     // month: "%b-%y"
            //     hour: "%e-%b-%y"
            // }
        } as Highcharts.XAxisOptions,
        yAxis: {
            title: {
                text: "Value"
            }
        },
        // legand: {
        //     enabled: false
        // },
        tooltip: {
            // valueDecimals: 2,
            // valueSuffix: " °C",
            // pointFormat: '{series.name} had stockpiled <b>{point.y:,.0f}</b><br/>warheads in {point.x}'
        },

        plotOptions: {
            area: {
                // fillColor: {
                //     linearGradient: { x1: 0, y1: 0, x2: 0, y2: 1 },
                //     stops: [
                //         [0, Highcharts.getOptions()?.colors?.[0]],
                //         [1, new Highcharts.Color(Highcharts.getOptions().colors?.[0]!).setOpacity(0).get('rgba')]
                //     ]
                // },
                marker: {
                    radius: 2
                },
                lineWidth: 1,
                states: {
                    hover: {
                        lineWidth: 1
                    }
                },
                threshold: null
            }
        },
        series: [
            {
                type: 'spline',
                name: `${this.onlyDateFormating(new Date())} - ${this.onlyDateFormating(new Date())}`,
                pointInterval: 24 * 360000 * 30,
                pointStart: new Date(new Date()).getTime(), //Date.UTC(2006, 0, 1)
                data: []
            } as Highcharts.PointOptionsObject
        ] as Highcharts.SeriesOptionsType[]
    };

    chartCallback: Highcharts.ChartCallbackFunction = chart => {
        this.chartRef = chart;
    };

    addSeries(): void {
        setTimeout(() => {
            this.chartRef.addSeries({
                type: 'spline',
                zoomType: 'x',
                name: `${this.onlyDateFormating(this.selectDate[0])} - ${this.onlyDateFormating(this.selectDate[1])}`,
                pointInterval: 24 * 360000,
                pointStart: new Date(this.selectDate[0]).getTime(),
                data: this.readingList
            });
            this.blockUI.stop();
        }, 3000);
    }

    removeSeries() {
        this.chartRef.series[0].destroy();
    }


    //#endregion Chart

    dateFormating(inputDate: Date): string {
        return (`${inputDate.getFullYear()}/${inputDate.getMonth() + 1}/${inputDate.getDate()} ${inputDate.getHours()}:${inputDate.getMinutes()}:${inputDate.getSeconds()}`).toString();
    }

    onlyDateFormating(inputDate: Date): string {
        return (`${inputDate.getFullYear()}/${inputDate.getMonth() + 1}/${inputDate.getDate()}`).toString();
    }

    limitDaysRangeSelection(): boolean {
        if (this.selectDate != null) {
            if (this.selectDate[0] !== null) {
                const date1: any = new Date(this.selectDate[0]);
                const date2: any = new Date(this.selectDate[1]);
                const diffTime = Math.abs(date2 - date1);
                const diffDays = Math.ceil(diffTime / (1000 * 60 * 60 * 24));
                // console.log(diffTime + " milliseconds");
                // console.log(diffDays + " days");

                if (diffDays > 5) {
                    this.notyfy.showError("please selected date must be within 5 days")
                    return false;
                }
            }
        }
        return true;
    }



}
