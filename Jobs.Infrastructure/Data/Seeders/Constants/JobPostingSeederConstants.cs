using Jobs.Domain.Enums;

namespace Jobs.Infrastructure.Data.Seeders.Constants
{
    public static class JobPostingSeederConstants
    {
        // Used AI to generate this data as it's a lot of work to do manually.
        public static readonly Dictionary<JobCategory, string[]> JobTitlesByCategory = new()
        {
            [JobCategory.IT] = [
                "Software Developer", "Frontend Developer", "Backend Developer", "Full Stack Developer",
                "DevOps Engineer", "Data Engineer", "Data Scientist", "Machine Learning Engineer",
                "Cloud Architect", "Network Administrator", "System Administrator", "IT Support Specialist",
                "QA Engineer", "Test Automation Engineer", "Product Manager", "Project Manager",
                "Scrum Master", "Product Owner", "UX/UI Designer", "Mobile Developer",
                "C# Developer", ".NET Developer", "Java Developer", "Python Developer"
            ],
            [JobCategory.Administrative] = [
                "Administrative Assistant", "Executive Assistant", "Office Manager", "Receptionist",
                "Data Entry Clerk", "Office Coordinator", "Secretary", "Office Administrator",
                "File Clerk", "Records Manager", "Document Controller", "Administrative Coordinator"
            ],
            [JobCategory.Financial] = [
                "Accountant", "Financial Analyst", "Bookkeeper", "Financial Controller",
                "Financial Manager", "Auditor", "Tax Consultant", "Payroll Specialist",
                "Credit Analyst", "Risk Manager", "Insurance Specialist", "Financial Advisor"
            ],
            [JobCategory.Educational] = [
                "Teacher", "Tutor", "Professor", "Educational Consultant",
                "Principal", "School Administrator", "Curriculum Developer", "Instructional Designer",
                "Special Education Teacher", "Education Coordinator", "Academic Advisor", "School Counselor"
            ],
            [JobCategory.Healthcare] = [
                "Nurse", "Doctor", "Medical Assistant", "Physical Therapist",
                "Occupational Therapist", "Pharmacist", "Dental Hygienist", "Medical Receptionist",
                "Healthcare Administrator", "Medical Laboratory Technician", "Radiologist", "Surgeon"
            ],
            [JobCategory.Other] = [
                "Marketing Specialist", "Sales Representative", "Customer Service Representative", "HR Manager",
                "Graphic Designer", "Content Writer", "Social Media Manager", "Warehouse Worker",
                "Driver", "Retail Associate", "Security Officer", "Maintenance Technician"
            ],
        };

        public static readonly Dictionary<JobCategory, string[]> CategoryResponsibilities = new()
        {
            [JobCategory.IT] = [
                "Develop and maintain software applications",
                "Write clean, efficient, and well-documented code",
                "Troubleshoot and debug software issues",
                "Participate in code reviews and design discussions",
                "Implement security and data protection measures",
                "Integrate software components and third-party programs",
                "Design and implement database structures",
                "Monitor system performance and make improvements"
            ],
            [JobCategory.Administrative] = [
                "Manage office supplies inventory and place orders as needed",
                "Coordinate meetings and take detailed minutes",
                "Maintain filing systems, both electronic and physical",
                "Answer and direct phone calls, emails, and visitors",
                "Prepare regular reports and presentations",
                "Assist with travel arrangements and expense reports",
                "Schedule appointments and maintain calendars",
                "Process invoices and track payments"
            ],
            [JobCategory.Financial] = [
                "Prepare financial statements and reports",
                "Conduct financial forecasting and risk analysis",
                "Develop and maintain budgets",
                "Ensure compliance with financial regulations",
                "Process accounts payable and receivable",
                "Reconcile bank statements and financial discrepancies",
                "Coordinate with external auditors",
                "Implement financial policies and procedures"
            ],
            [JobCategory.Educational] = [
                "Develop and deliver engaging lesson plans",
                "Assess student progress and provide constructive feedback",
                "Create and maintain a positive learning environment",
                "Communicate effectively with students, parents, and colleagues",
                "Adapt teaching methods to accommodate different learning styles",
                "Participate in professional development opportunities",
                "Maintain accurate student records",
                "Develop curriculum materials and resources"
            ],
            [JobCategory.Healthcare] = [
                "Provide patient care according to established protocols",
                "Maintain accurate patient records and documentation",
                "Collaborate with healthcare team members",
                "Administer medications and treatments as prescribed",
                "Monitor patient condition and report changes",
                "Educate patients and families about health conditions and care",
                "Ensure compliance with healthcare regulations and standards",
                "Participate in quality improvement initiatives"
            ],
            [JobCategory.Other] = [
                "Develop and implement strategic plans",
                "Build and maintain client relationships",
                "Create and deliver presentations to stakeholders",
                "Monitor industry trends and competitor activities",
                "Prepare and analyze reports",
                "Maintain accurate records and documentation",
                "Handle inquiries and resolve issues promptly",
                "Coordinate with internal departments to ensure goals are met"
            ],
        };

        public static readonly Dictionary<JobCategory, string[]> CategoryRequirements = new()
        {
            [JobCategory.IT] = [
                "Bachelor's degree in Computer Science or related field",
                "Experience with relevant programming languages",
                "Knowledge of software development methodologies",
                "Experience with database systems and SQL",
                "Understanding of web technologies and protocols",
                "Version control system experience (Git preferred)",
                "Problem-solving and debugging skills",
                "Knowledge of CI/CD pipelines"
            ],
            [JobCategory.Administrative] = [
                "Previous experience in an administrative role",
                "Proficiency in Microsoft Office Suite",
                "Excellent organizational and multitasking abilities",
                "Knowledge of office management systems and procedures",
                "Strong written and verbal communication skills",
                "Experience with data entry and database management",
                "Customer service orientation",
                "Ability to maintain confidentiality"
            ],
            [JobCategory.Financial] = [
                "Bachelor's degree in Finance, Accounting, or related field",
                "Professional certification (CPA, CFA, etc.) preferred",
                "Experience with financial reporting and analysis",
                "Knowledge of accounting principles and practices",
                "Experience with financial software and systems",
                "Attention to detail and analytical mindset",
                "Understanding of regulatory requirements",
                "Advanced Excel skills"
            ],
            [JobCategory.Educational] = [
                "Bachelor's or Master's degree in Education or relevant field",
                "Teaching certification or license",
                "Experience working with diverse student populations",
                "Knowledge of current educational methodologies",
                "Ability to create engaging learning materials",
                "Classroom management skills",
                "Experience with educational technology",
                "Understanding of student assessment methods"
            ],
            [JobCategory.Healthcare] = [
                "Relevant healthcare degree or certification",
                "Current professional license or registration",
                "Clinical experience in relevant specialty",
                "Knowledge of medical terminology and procedures",
                "Understanding of healthcare regulations and standards",
                "Experience with electronic health record systems",
                "Strong patient care and communication skills",
                "Basic life support certification"
            ],
            [JobCategory.Other] = [
                "Relevant industry experience",
                "Excellent communication and interpersonal skills",
                "Project management experience",
                "Computer literacy and proficiency in relevant software",
                "Problem-solving and critical thinking abilities",
                "Time management and organizational skills",
                "Team collaboration and leadership abilities",
                "Customer service orientation"
            ],
        };

        public static readonly string[] JobIntroductions =
        [
            "We are looking for a {0} to join our team.",
            "A great opportunity has opened up for a {0} at our company.",
            "Are you an experienced {0}? We want to hear from you!",
            "Join our team as a {0} and make an impact.",
            "Exciting opportunity for a {0} to work with our growing team."
        ];

        public static readonly string[] CompanyDescriptions =
        [
            "Our company is known for innovation and excellence in the industry.",
            "We are a fast-growing company with an inclusive, dynamic culture.",
            "We pride ourselves on our collaborative and supportive work environment.",
            "Our mission is to deliver exceptional solutions to our clients worldwide.",
            "We've been in business for over 10 years, serving clients across multiple industries."
        ];

        public static readonly string[] GenericResponsibilities =
        [
            "Collaborate effectively with cross-functional teams",
            "Manage projects from conception to completion",
            "Document processes and procedures",
            "Research and implement best practices",
            "Identify opportunities for improvement",
            "Provide regular reports and updates",
            "Maintain professional communication with stakeholders",
            "Participate in ongoing training and development",
            "Stay current with industry developments"
        ];

        public static readonly string[] GenericRequirements =
        [
            "Excellent communication skills",
            "Strong attention to detail",
            "Ability to work independently and in a team",
            "Problem-solving skills",
            "Time management skills",
            "Adaptability and flexibility",
            "Customer-focused mindset",
            "Analytical thinking",
            "Previous experience in a similar role"
        ];

        public static readonly string[] Benefits =
        [
            "Competitive salary",
            "Health insurance",
            "Retirement plan",
            "Flexible working hours",
            "Remote work possibilities",
            "Professional development opportunities",
            "Company events and team building",
            "Modern office space",
            "Casual dress code",
            "Free coffee and snacks",
            "Employee wellness program",
            "Paid time off",
            "Performance bonuses"
        ];

        public static readonly string[] Conclusions =
        [
            "If you're interested in this position, we'd love to hear from you!",
            "Apply today and join our talented team!",
            "Send your resume and cover letter to start the application process.",
            "We look forward to reviewing your application.",
            "Ready to take the next step in your career? Apply now!"
        ];

        private static readonly (string Label, int? MinHours, int? MaxHours)[] _workingHoursPatterns =
        [
            ("Full-time standard", 36, 40),
            ("Full-time with flexibility", 32, 40),
            ("Full-time intensive", 40, 45),

            ("Part-time standard", 16, 32),
            ("Part-time light", 8, 16),
            ("Variable part-time", 8, 24),

            ("Minimum hours only", 8, null),
            ("Minimum half-time", 16, null),
            ("Minimum substantial", 24, null),

            ("Fixed 40 hours", 40, 40),
            ("Fixed 32 hours", 32, 32),
            ("Fixed 24 hours", 24, 24),
            ("Fixed 20 hours", 20, 20),
            ("Fixed 16 hours", 16, 16)
        ];

        public static (string Label, int? MinHours, int? MaxHours)[] WorkingHoursPatterns => _workingHoursPatterns;
    }
}
