// src/store/useAssistanceStore.ts
import create from 'zustand';
import { Assistance, PagedResult } from '../shared/types/Models.ts';
import { getAssistances } from '../api/AssistanceApi.ts';

interface AssistanceState {
  assistances: Assistance[];
  totalCount: number;
  pageNumber: number;
  pageSize: number;
  totalPages: number;
  addAssistance: (assistance: Assistance) => void;
  setAssistances: (assistances: PagedResult<Assistance>) => void;
  fetchAssistances: (pageNumber: number, pageSize: number) => Promise<void>;
}

export const useAssistanceStore = create<AssistanceState>((set) => ({
  assistances: [],
  totalCount: 0,
  pageNumber: 1,
  pageSize: 10,
  totalPages: 0,
  addAssistance: (assistance) => set((state) => ({
    assistances: [...state.assistances, assistance],
  })),
  setAssistances: (pagedResult) => set({
    assistances: pagedResult.data,
    totalCount: pagedResult.totalCount,
    pageNumber: pagedResult.pageNumber,
    pageSize: pagedResult.pageSize,
    totalPages: pagedResult.totalPages,
  }),
  fetchAssistances: async (pageNumber: number, pageSize: number) => {
    try {
      const response = await getAssistances(pageNumber, pageSize);
      if (response.code === 'success' && response.data) {
        set({
          assistances: response.data.data,
          totalCount: response.data.totalCount,
          pageNumber: response.data.pageNumber,
          pageSize: response.data.pageSize,
          totalPages: response.data.totalPages,
        });
      } else {
        console.error('Error fetching assistances:', response.error?.message || 'Unknown error');
      }
    } catch (error) {
      console.error('Fetch assistances failed:', error);
    }
  },
}));
