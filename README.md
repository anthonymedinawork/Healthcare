## About
This MVC project is an ASP.NET Core based website about a hospital management system where users can view information about the hospital, their visits and doctor appointments.
The project supports three roles: Admin, Moderator and User.

## Roles
The Administrator has access to absolutely everything on the site - he can view, create, delete and update all objects on the site for which such functionality is implemented. He can also create doctor and patient accounts. Each registration on the site creates an account of an ordinary user (patient), and doctor accounts are created only by the administrator.
Moderators are all doctors on the site, they only have access to their patients, appointments and visits and can view, delete and create new ones. Doctors also have access to the entire database of drugs for which an external API is used as a source of information.
The Patient account allows viewing personal appointments and visits with the GP, as well as deleting created appointments.
People without an account on the site can only access the Home and About pages.

## Menu
The menu contains links to Home, About, Doctors, Patients, Appointments, Attendances and Medications
### Home and About page
They contain basic information about the hospital's website
### Doctors page
Contains a list of all doctors in the system and their basic information, form to create a new doctor, to update information, to delete and details
### Patients page
Consists of a list of all patients in the system and their basic information, a form to create a new patient, to update information, to delete and details
### Appointments page
Contains a list of all saved times in the system and their basic information, form to create a new time, to delete and details
### Attendances page
It consists of a list of all visits made to the system and the basic information about them, a form for creating a new visit, for deletion and details
### Medications page
Contains a list of all drugs in the database, with information obtained from an external API
