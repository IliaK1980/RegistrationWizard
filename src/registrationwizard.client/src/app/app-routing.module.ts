import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { Step1Component } from './components/step1/step1.component';
import { Step2Component } from './components/step2/step2.component';

const routes: Routes = [
  { path: '', redirectTo: '/step1', pathMatch: 'full' }, // Redirect to step1 by default
  { path: 'step1', component: Step1Component },
  { path: 'step2', component: Step2Component }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
