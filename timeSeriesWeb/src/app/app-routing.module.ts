import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DashboardComponent } from './dashboard/dashboard.component';
import { DashboardSpComponent } from './dashboardsp/dashboardsp.component';

const routes: Routes = [
    { path: '', redirectTo: 'dashboard', pathMatch: 'full' }
    , { path: 'dashboard', component: DashboardComponent }
    , { path: 'dashboardsp', component: DashboardSpComponent }
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class AppRoutingModule { }
