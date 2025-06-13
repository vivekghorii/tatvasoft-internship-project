import { Component, OnDestroy, OnInit } from "@angular/core"
import {
  AbstractControl,
  FormBuilder,
  FormControl,
  FormGroup,
  ReactiveFormsModule,
  ValidationErrors,
  Validators,
} from "@angular/forms"
import { Router } from "@angular/router"
import { NgToastService } from "ng-angular-popup"
import { ToastrService } from "ngx-toastr"
import { APP_CONFIG } from "src/app/main/configs/environment.config"
import { AuthService } from "src/app/main/services/auth.service"
import { HeaderComponent } from "../../header/header.component"
import { SidebarComponent } from "../../sidebar/sidebar.component"
import { NgIf } from "@angular/common"
import { Subscription } from "rxjs"

@Component({
  selector: "app-add-user",
  standalone: true,
  imports: [SidebarComponent, HeaderComponent, ReactiveFormsModule, NgIf],
  templateUrl: "./add-user.component.html",
  styleUrls: ["./add-user.component.css"],
})
export class AddUserComponent implements OnInit, OnDestroy {
  private unsubscribe: Subscription[] = [];
  loading = false;
  
  constructor(
    private _fb: FormBuilder,
    private _service: AuthService,
    private _router: Router,
    private _toast: NgToastService,
  ) {}
  registerForm: FormGroup
  formValid: boolean
  get isFormValid(): boolean {
    return this.registerForm.valid && !this.loading;
  }

  ngOnInit(): void {
    this.createRegisterForm()
  }

  createRegisterForm() {
    this.registerForm = this._fb.group(
      {
        firstName: [null, Validators.compose([Validators.required])],
        lastName: [null, Validators.compose([Validators.required])],
        phoneNumber: [
          null,
          Validators.compose([Validators.required, Validators.minLength(10), Validators.maxLength(10)]),
        ],
        emailAddress: [null, Validators.compose([Validators.required, Validators.email])],
        password: [null, Validators.compose([Validators.required, Validators.minLength(5), Validators.maxLength(10)])],
        confirmPassword: [null, Validators.compose([Validators.required])],
      },
      { validator: [this.passwordCompareValidator] },
    )
  }

  passwordCompareValidator(fc: AbstractControl): ValidationErrors | null {
    return fc.get("password")?.value === fc.get("confirmPassword")?.value ? null : { notmatched: true }
  }
  
  get firstName() {
    return this.registerForm.get("firstName") as FormControl
  }
  get lastName() {
    return this.registerForm.get("lastName") as FormControl
  }
  get phoneNumber() {
    return this.registerForm.get("phoneNumber") as FormControl
  }
  get emailAddress() {
    return this.registerForm.get("emailAddress") as FormControl
  }
  get password() {
    return this.registerForm.get("password") as FormControl
  }
  get confirmPassword() {
    return this.registerForm.get("confirmPassword") as FormControl
  }

  onSubmit() {
    if (this.registerForm.valid && !this.loading) {
      this.loading = true;
      const register = this.registerForm.value;
      register.userType = "user";
      
      const registerUserSubscribe = this._service.registerUser(register).subscribe(
        (data: any) => {
          this.loading = false;
          if (data.result == 1) {
            this._toast.success({ detail: "SUCCESS", summary: data.data, duration: APP_CONFIG.toastDuration });
            setTimeout(() => {
              this._router.navigate(["admin/user"]);
            }, 1000);
          } else {
            this._toast.error({ detail: "ERROR", summary: data.message, duration: APP_CONFIG.toastDuration });
          }
        },
        (error) => {
          this.loading = false;
          this._toast.error({ detail: "ERROR", summary: error.message || "An error occurred while adding user", duration: APP_CONFIG.toastDuration });
        }
      );
      this.unsubscribe.push(registerUserSubscribe);
    } else {
      this.formValid = true;
    }
  }

  onCancel() {
    this._router.navigateByUrl
    ("admin/user")
  }

  ngOnDestroy() {
    this.unsubscribe.forEach((sb) => sb.unsubscribe());
  }
}