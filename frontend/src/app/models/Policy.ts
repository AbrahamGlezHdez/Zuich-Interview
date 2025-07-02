export interface Policy {
  id: number;
  policyNumber: string;
  type: string;
  startDate: string;
  expirationDate: string;
  amount: number;
  status:number;
  clientId: number;
  clientName?: string;
}
