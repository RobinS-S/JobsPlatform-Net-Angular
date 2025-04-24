export * from './company.service';
import { CompanyService } from './company.service';
export * from './jobPosting.service';
import { JobPostingService } from './jobPosting.service';
export const APIS = [CompanyService, JobPostingService];
