export interface Assistance {
    id: string;
    userProfileId: string;
    assistanceCategoryId: string;
    title?: string;
    assistanceCategory?: AssistanceCategory;
    description?: string;
    price: number;
    durationInMinutes: number;
    rating: number;
    location?: string;
    reviews?: Review[];
    createdAt: string;
    updatedAt: string;
    userProfile?: UserProfile;
  }
  
  export interface AssistanceCategory {
    id: string;
    name?: string;
    description?: string;
    createdAt: string;
    updatedAt: string;
    assistances?: Assistance[];
  }
  
  export interface UserProfile {
    id: string;
    name?: string;
    email?: string;
    photoURL?: string;
    phoneNumber?: string;
    rating: number;
    createdAt: string;
    updatedAt: string;
    assistances?: Assistance[];
    reviews?: Review[]; 
  }
  
  export interface Review {
    id: string;
    userProfileId: string;
    assistanceId: string;
    rating: number;
    comment: string;
    createdAt: string;
  }
  export interface CreateReviewViewModel  {
  
    assistanceId: string;
    userProfileId: string;
    comment:string;
    rating: number;
  };
  export interface AssistanceViewModel {
    id: string;
    userProfileId: string;
    assistanceCategoryId: string;
    title?: string;
    assistanceCategoryName?: string;
    description?: string;
    price: number;
    durationInMinutes: number;
    rating: number;
    location?: string;
    reviews?: ReviewViewModel[];
    createdAt: string;
    updatedAt: string;
    userProfilePhotoUrl?: string;
    userProfilePhoneNumber?: string;
  }
  
  export interface CreateAssistanceViewModel {
    userProfileId: string;
    assistanceCategoryId: string;
    title?: string;
    description?: string;
    price: number;
    durationInMinutes: number;
    location?: string;
  }
  
  export interface ReviewViewModel {
    id: string;
    assistanceId: string;
    userProfileId: string;
    rating: number;
    comment?: string;
    createdAt: string;
    updatedAt: string;
  }
  
  export interface ViewModel {
    userProfileId: string;
    assistanceId: string;
    rating: number;
    comment?: string;
  }
  
  export interface BaseResponse<T> {
    code: 'success' | 'error';
    data?: T;
    error?: {
      message: string;
      details?: any;
    };
  }
  export interface PagedResult<T> {
    pageNumber: number;
    pageSize: number;
    totalCount: number;
    totalPages: number;
    data: T[];
  }

  export interface OrderRequest {
    id: string;
    serviceId: string;
    customerId: string;
    durationInMinutes: number;
    description?: string;
    status: OrderRequestStatus;
    createdAt: string;
    updatedAt: string;
}

export interface Order {
  id: string;
  serviceId: string;
  customerId: string;
  status: OrderStatus;
  description?: string;
  durationInMinutes: number;
  scheduledDate: string;  
  price: number;
  createdAt: string;
  updatedAt: string;
}

export interface CreateOrderViewModel {
  customerId: string;
  serviceId: string;
  description?: string;
  baseRatePerMinute: number;
  baseRateDurationInMinutes: number;
  durationInMinutes: number;
  scheduledDate: string;  
}

export interface CreateOrderRequestViewModel {
  description?: string;
}
export enum OrderRequestStatus {
  Pending = 'Pending',
  Approved = 'Approved',
  Rejected = 'Rejected',
  Cancelled = 'Cancelled',
}
export enum OrderStatus {
  Pending = 'Pending',
  Confirmed = 'Confirmed',
  InProgress = 'InProgress',
  Completed = 'Completed',
  Cancelled = 'Cancelled',
}
export interface CreateUserProfileViewModel {
  name: string;
  email: string;
  password: string;
  photoURL?: string; // Если не обязательно
  phoneNumber?: string; // Если не обязательно
}

export interface RegisterResponse {
  auth0Id: string;
  error?: string;
}