<h1>Placemate</h1>

<P>Placemate is a Placement Management System that bridges the gap between students, companies, and placement coordinators (admin) through a smart, skill-based job posting and eligibility verification system.
It automates the recruitment workflow, enabling controlled authorization, eligibility-based filtering, and transparent job status tracking.</P>

<h2>Project Overview</h2>
Placemate streamlines the campus placement process through three user roles:
<ul>
  <li>Admin – Acts as the placement coordinator; approves companies, manages system skills, and handles user operations.</li>
  <li>Company – Can post jobs only after admin approval and manage student selection.</li>
  <li>Student – Can browse and apply to eligible jobs and track their application status.</li>
</ul>
The system ensures secure authentication, authorization, and role-based access control, maintaining transparency and data integrity.


<h2>Features Implemented</h2>
<h3>🔐 Authentication & Authorization</h3>
<ul>
  <li>Secure registration and login for Students, Companies, and Admin.</li>
  <li>Role-based routing and authorization.</li>
  <li>Companies require admin approval before posting jobs.</li>
</ul>

<h3>🏢 Company Module</h3>
<ul>
  <li>Post jobs after admin approval.</li>
  <li>Select predefined system skills from dropdowns when creating jobs.</li>
  <li>Define eligibility filters: CGPA, 10th %, 12th %, and required skills.</li>
  <li>View and manage applicants.</li>
  <li>Select students for jobs (checkbox selection).</li>
  <li>Once a job is finalized, further applications are locked.</li>
  <li>View final list of selected students.</li>
</ul>

<h3>🎓 Student Module</h3>
<ul>
  <li>Instant dashboard access upon signup (no admin approval needed).</li>
  <li>View all job postings with:</li>
  <ul>
    <li>Job skills</li>
    <li>Matched skills</li>
    <li>Remaining skills</li>
  </ul>
  <li>Eligibility automatically computed and displayed per job.</li>
  <li>Apply to eligible jobs.</li>
  <li>Track job application and selection status in a separate “Job Offers” tab.</li>
</ul>

<h3>👨‍💼 Admin Module</h3>
<ul>
  <li>Approves or rejects new companies.</li>
  <li>Manages all CRUD operations on Skills.</li>
  <li>Handles user deletions (students or companies).</li>
</ul>


<h2>🚀 Setup Instructions (.NET Core 3.1.1)</h2>
Ensure the following are installed:
<ul>
  <li>.NET Core SDK 3.1.1 or later</li>
  <li>Visual Studio</li>
  <li>Git</li>
</ul>

<h4>Steps to run project:</h4>
<ol>
  <li><h4>Clone the repository:</h4>
    <b><u>By run following command on terminal:</u></b><br>
  git clone https://github.com/PatelTaksh2006/Skillora.git<br>
  cd Skillora<br>
  </li>
  <li><h4>Configure Database Connection</h4>
  <b><u>Open appsettings.json and modify your SQL Server connection string:</u></b><br>
    "ConnectionStrings": {<br>
  "DbCon": "server=(localdb)\\MSSQLLocalDB;database=<your_database_name>;Trusted_Connection=true"<br>
}
  </li>
    <li><h4>run the migrations:</h4>
    <b><u>Open the Package Manager Console in Visual Studio and run:</u></b><br>
      Update-Database
    </li>
    <li><h4>Run the project:</h4>
    <b><u>By pressing crtl + F5 in visual studio or manually press that run button.</u></b>
    </li>
</ol>
<br><br>

<h2>👥 Team Members & Contributions</h2>
<table>
  <tr>
    <th>Name</th>
    <th>Identity No.</th>
    <th>Contribution:</th>
  </tr>
  <tr>
    <td>Patel Taksh</td>
    <td>23CEUOS121</td>
    <td>Implemented all Student and Job-related modules — authentication, job listing logic, eligibility checks, student selection flow, and job status management.</td>
  </tr>
  <tr>
    <td>Patel Rutul</td>
    <td>23CEUOZ118</td>
    <td>Implemented all Skills and Company module,including crud operations for skills and company registration and admin approval.</td>
  </tr>
</table>
