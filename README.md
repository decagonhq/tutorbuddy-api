# TutorBuddy - connecting students with tutors

## introduction
We live in an ever changing world, and with technology people are constantly learning new skills, assisting others, and providing services. 

There is always someone who has a skill to share and someone who is willing to learn that skill.

**TutorBuddy** is an application that connects tutors with students.
The application is built around three main concepts: **tutors**, **students** and **sessions**.

## student
- Student is a individual who needs assistance in some form
- Learn a new skill or technology
- needs help setting up a device
- needs help reparing for presentation
- wants to learn to fly an airplane

## tutor
- Tutor has a set of skills and expertise to offer
- can help an individual to setup device or learn a new skill or technology
- can provide a legal or counseling service

## session
- Session is an interaction outside the app itself
- Sessions have duration
- Upon completion, sessions can be rated by tutor and student
- Reporting can be done (future enhancement)
  - Report on most sought after topics or subjects
  - Report on most active tutors
  - Report Avg length of sessions

## platforms
The application relies on a backend API written in C#, Dotnet Core, and Entity Framework Core. The initial release was to support two front end applications:
 - web front end with React targeting devices with larger screen sizes (desktop and laptops), 
 - iOs version for apple mobile devices.

### high level architecture
![high level architecture](https://github.com/kowusu01/GenericProjectDoc/blob/main/images/Architecture.jpg?raw=true)

### backend api architecture
The api backend is based on a simple layered architecture with just three layers.  
![backend api architecture](https://github.com/kowusu01/GenericProjectDoc/blob/main/images/app-arch.jpg?raw=true)

## core application functions
The core functions of the app include the following:
- Create user (student/tutor)
- Authenticate user
- User search for tutor, request a session
- Tutor accepts request and begins session
- Tutor or student ends session
- Tutor or student provide rating

## building the application

### technologies
![technologies used](https://github.com/kowusu01/GenericProjectDoc/blob/main/images/tech-stack-scaled.PNG?raw=true)

### login process using 2FA
![login process - sequence diagram](https://github.com/kowusu01/GenericProjectDoc/blob/main/images/login-with-simple-2fa.jpg?raw=true)


## future enhancements
- Think of tutors as resource that provide services
- Students as target audience, can be a group
- App can be used for resource requests and tracking
- Legal counseling as sessions
- Speaking engagements
- One tutor can engage with a group
- Payment integration
- Authentication as a service
- Reporting
  - Report most sought after topics or subjects
  - Report most active tutors
  - Avg length of sessions


