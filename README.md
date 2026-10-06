## HealthcareSolutions



HealthcareSolutions is a web-based healthcare management system designed to centralize patient, practitioner, appointment, medical record, medication, prescription, and staff management.



The system uses a role-based access model to control access to healthcare information and administrative functions.



##### &#x20;Project Structure





HealthcareSolutions/

│

├── HealthcareSolutions.API/

│   ├── Controllers/

│   ├── Data/

│   ├── DTOs/

│   ├── Enums/

│   ├── Interfaces/

│   ├── Models/

│   ├── Services/

│   └── Program.cs

│

├── HealthcareSolutions.Client/

│   ├── Authentication/

│   ├── Models/

│   ├── Pages/

│   ├── Services/

│   ├── Shared/

│   └── Program.cs

│

└── HealthcareSolutions.sln





##### Technology Stack



###### Backend



\* ASP.NET Core Web API

\* .NET 8

\* C#

\* Entity Framework Core 8

\* SQL Server

\* JWT authentication

\* Role-based authorization

\* Swagger / OpenAPI



###### Frontend



\* Blazor WebAssembly

\* .NET 9

\* C#

\* HTML

\* CSS



###### Development Tools



\* Visual Studio

\* PowerShell

\* Entity Framework Core CLI

\* SQL Server

\* Git

\* GitHub

\* Swagger



###### System Architecture



HealthcareSolutions follows a client-server architecture.





┌──────────────────────────────┐

│   Blazor WebAssembly Client  │

│          .NET 9              │

└──────────────┬───────────────┘

&#x20;              │ HTTP / JSON

&#x20;              │ JWT Bearer

&#x20;              ▼

┌──────────────────────────────┐

│       ASP.NET Core API       │

│           .NET 8             │

└──────────────┬───────────────┘

&#x20;              │ Entity Framework Core

&#x20;              ▼

┌──────────────────────────────┐

│          SQL Server          │

│    HealthcareSolutionsDb     │

└──────────────────────────────┘

```



###### User Roles



The system supports four staff roles:



| Role         | Main Responsibilities                                                            |

| ------------ | -------------------------------------------------------------------------------- |

| Admin        | System administration, staff management, catalogue management and overall access |

| Receptionist | Patient and appointment administration                                           |

| Practitioner | Patient care, medical records, visit notes and prescriptions                     |

| Pharmacist   | Medication catalogue management and prescription dispensing                      |



Access to protected API endpoints is controlled through JWT authentication and role-based authorization.



###### Authentication



HealthcareSolutions uses JWT bearer authentication.



**When a user successfully logs in:**



1\. The client sends the username and password to the authentication API.

2\. The API validates the credentials.

3\. The API generates a JWT containing the user's identity and role.

4\. The client stores the authenticated session in browser session storage.

5\. API requests include the JWT as a Bearer token.

6\. The API validates the token before allowing access to protected endpoints.



Passwords are stored using password hashes rather than plaintext passwords.



The login page also provides a password visibility toggle so users can verify their password before submitting the login form.



**Current Staff Account Model**



For the current MVP, staff accounts are provisioned by an administrator.



The administrator:



\* Creates the staff account.

\* Assigns the staff role.

\* Sets the initial password.



The staff member then uses those credentials to access the system.



This approach was retained for the MVP because the authentication and authorization workflow is fully implemented and tested.



###### Planned Authentication Enhancement



A future version will introduce an administrator-controlled invitation workflow.



**The planned workflow is:**





Admin

&#x20; │

&#x20; │ Creates staff invitation

&#x20; │ Assigns role

&#x20; ▼

Staff invitation

&#x20; │

&#x20; ▼

Staff member

&#x20; │

&#x20; ├── Chooses username

&#x20; ├── Creates private password

&#x20; └── Activates account

&#x20; │

&#x20; ▼

Staff account





The administrator will retain control over the staff member's role and permissions, while the staff member will control their own username and password.



###### Core Features



**Patient Management**



\* Create patient records.

\* View patient information.

\* Update patient information.

\* Manage patient contact details.

\* Associate patients with medical records and appointments.



**Practitioner Management**



\* Manage practitioner profiles.

\* Associate practitioners with staff accounts.

\* Store practitioner specialty information.

\* Track practitioner availability.



**Appointment Management**



\* Create appointments.

\* Associate appointments with patients and practitioners.

\* View appointment information.

\* Reschedule appointments.

\* Cancel appointments.

\* Prevent overlapping appointments through backend validation.



**Medical Records**



Each patient can have a medical record containing the patient's ongoing clinical information.



The system enforces a one-to-one relationship between patients and medical records.



**Visit Notes**



Practitioners can create and manage visit notes associated with:



\* Medical records

\* Practitioners

\* Appointments



**Visit notes support:**



\* Clinical notes

\* Diagnosis

\* Treatment

\* Creation date



**Medication Catalogue**



The medication catalogue stores available medications and their dosage information.



Administrators and pharmacists manage the catalogue.



Practitioners can view available medications when creating prescriptions.





**Medication Requests**



Practitioners can request medications that do not currently exist in the catalogue.





The workflow is:



Practitioner

&#x20;    │

&#x20;    │ Medication request

&#x20;    ▼

Medication Request

&#x20;    │

&#x20;    ├── Approved ──► Medication added to catalogue

&#x20;    │

&#x20;    └── Rejected



Administrators and pharmacists review medication requests.



**Prescriptions**



Practitioners create prescriptions using catalogue medications.



A prescription requires:



\* Patient medical record

\* Practitioner

\* Appointment

\* Medication

\* Dosage

\* Frequency

\* Duration



Prescription statuses include:



\* Pending

\* Dispensed

\* Unavailable



Pharmacists manage prescription dispensing status.



**Database**



The application uses SQL Server with Entity Framework Core.



The main database is:





**HealthcareSolutionsDb**





The application uses:



**ApplicationDbContext**



###### 

###### Main Entities



\* User

\* Patient

\* Practitioner

\* Appointment

\* MedicalRecord

\* VisitNote

\* Medication

\* MedicationRequest

\* Prescription



###### Entity Relationships



User

&#x20;│

&#x20;└── Practitioner



Patient

&#x20;│

&#x20;├── MedicalRecord

&#x20;└── Appointments



Practitioner

&#x20;│

&#x20;├── Appointments

&#x20;├── VisitNotes

&#x20;└── Prescriptions



MedicalRecord

&#x20;│

&#x20;├── VisitNotes

&#x20;└── Prescriptions



Appointment

&#x20;│

&#x20;├── VisitNote

&#x20;└── Prescriptions



Medication

&#x20;│

&#x20;├── Prescriptions

&#x20;└── MedicationRequests



###### 

###### Entity Framework Migrations



The project currently contains two migrations:





**20260928213640\_InitialCreate**

**20261005132612\_AddMedicationRequests**





The database has been verified against the migrations and is currently up to date.



To inspect migrations:



**PowerShell**

dotnet ef migrations list --context ApplicationDbContext





To update the database:



**PowerShell**

dotnet ef database update --context ApplicationDbContext





###### Running the Application



**Prerequisites**



Install:



\* .NET 8 SDK

\* .NET 9 SDK

\* SQL Server

\* Visual Studio or another compatible .NET development environment



The required .NET SDK versions should match the target frameworks used by the API and client.



###### Configure the API



Open the API project's configuration and provide the SQL Server connection string and JWT configuration.



The API expects configuration values for:





**ConnectionStrings:DefaultConnection**

**Jwt:Key**

**Jwt:Issuer**

**Jwt:Audience**





The JWT key should be kept private and should not be committed to source control.



###### Run the API



Open Developer PowerShell and run:



**PowerShell**

cd HealthcareSolutions.API

dotnet restore

dotnet build

dotnet run





The development HTTP API runs on:





**http://localhost:5292**





Swagger is available at:





**http://localhost:5292/swagger**



###### Run the Client



Open another Developer PowerShell window:



**PowerShell**

cd HealthcareSolutions.Client

dotnet restore

dotnet build

dotnet run



The development client runs on:



**http://localhost:5063**





The client communicates with the API through the configured API base address.



###### API Documentation



Swagger/OpenAPI is enabled in the Development environment.



The API exposes JWT authentication through the Swagger authorization interface.



**To authenticate through Swagger:**



1\. Log in through the authentication endpoint.

2\. Copy the returned JWT.

3\. Select Authorize in Swagger.

4\. Enter the Bearer token.

5\. Test protected endpoints.



###### Testing



The application has been tested across the main authentication, authorization, patient, appointment, medical record, visit note, medication, medication request, and prescription workflows.



The final API and client builds complete successfully after clean builds.



**API:**



dotnet clean

dotnet build





Client:





dotnet clean

dotnet build





Both projects currently build successfully with zero build errors.



###### Security



**HealthcareSolutions uses several security controls:**



\* JWT bearer authentication

\* Role-based authorization

\* Password hashing

\* Protected API endpoints

\* CORS configuration

\* Server-side authorization checks

\* Client-side role-aware UI

\* Session-based token storage

\* Database foreign-key restrictions

\* Backend validation for protected operations



Client-side role restrictions are treated as a user interface concern. The API also enforces authorization so users cannot rely on the client interface alone to gain access to protected operations.



###### Current MVP Scope



**The current MVP focuses on:**



\* Staff authentication

\* Role-based access

\* Patient management

\* Practitioner management

\* Appointment management

\* Medical records

\* Visit notes

\* Medication catalogue

\* Medication requests

\* Prescription management

\* Prescription dispensing workflow



###### Future Enhancements



Potential future improvements include:



\* Staff invitation and account activation workflow

\* Staff-created usernames and passwords

\* Password reset functionality

\* Email verification

\* Improved account lifecycle management

\* Production HTTPS configuration

\* Automated integration testing

\* Audit logging

\* Appointment notifications

\* Patient-facing access

\* Expanded reporting

\* Production deployment configuration



###### Development Status



HealthcareSolutions has reached the final MVP cleanup and release-preparation stage.



Current status:





Authentication             Complete

Authorization              Complete

Patient Management         Complete

Practitioner Management    Complete

Appointments               Complete

Medical Records            Complete

Visit Notes                Complete

Medication Catalogue       Complete

Medication Requests        Complete

Prescriptions              Complete

Password Visibility        Complete

Database Migrations        Complete

Debug Code Cleanup         Complete

API Build                  Passing

Client Build               Passing

Functional Testing         Passing

Documentation              Finalizing





###### Project Information



Solution:



**HealthcareSolutions.sln**



Projects:



**HealthcareSolutions.API**

**HealthcareSolutions.Client**





Backend target:

**.NET 8**



Frontend target:

**.NET 9**



Database:

**SQL Server**





ORM:

**Entity Framework Core 8**



Authentication:

**JWT Bearer Authentication**



Frontend:

**Blazor WebAssembly**



API documentation:

**Swagger / OpenAPI**





