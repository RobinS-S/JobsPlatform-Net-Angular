/**
 * Jobs API
 *
 * 
 *
 * NOTE: This class is auto generated by OpenAPI Generator (https://openapi-generator.tech).
 * https://openapi-generator.tech
 * Do not edit the class manually.
 */
import { JobCategory } from './jobCategory';


/**
 * Response DTO for a job posting.
 */
export interface JobPostingResponse { 
    id?: string;
    title?: string | null;
    description?: string | null;
    category?: JobCategory;
    minHoursPerWeek?: number | null;
    maxHoursPerWeek?: number | null;
    allowsRemoteWork?: boolean | null;
    contactEmail?: string | null;
    jobUrl?: string | null;
    companyId?: string;
    companyName?: string | null;
    createdAt?: string;
    updatedAt?: string | null;
}
export namespace JobPostingResponse {
}


