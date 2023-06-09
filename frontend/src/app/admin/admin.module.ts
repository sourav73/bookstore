import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AdminLoginComponent } from './admin-login/admin-login.component';
import { AdminRegisterComponent } from './admin-register/admin-register.component';
import { AdminRoutingModule } from './admin.routing.module';
import { DashboardComponent } from './dashboard/dashboard.component';

@NgModule({
  declarations: [AdminLoginComponent, AdminRegisterComponent, DashboardComponent],
  imports: [CommonModule, AdminRoutingModule],
})
export class AdminModule {}
