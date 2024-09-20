import { create } from 'zustand';
import { combine } from 'zustand/middleware';

import { Assistance, Review, UserProfile, Order } from '../shared/types/Models';
import { 
  // getAssistanceById, 
  createAssistance, 
} from '../api/AssistanceApi.ts';

export const useStore = create(
  combine(
    {
      assistance: null as Assistance | null,
      reviews: [] as Review[],
      userProfile: null as UserProfile | null,
      orders: [] as Order[],
    },
    (set) => ({
      // fetchAssistanceById: async (id: string) => {
      //   const response = await getAssistanceById(id);
      //   if (response.code === 'success' && response.data) {
      //     set({ assistance: response.data });
      //   }
      // },
      addAssistance: async (assistance: Assistance) => {
        const response = await createAssistance(assistance);
        if (response.code === 'success' && response.data) {
          set({ assistance: response.data });
        }
      },
    }
  )))
