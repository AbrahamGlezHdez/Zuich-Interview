export interface Client {
  id: number;
  identificationNumber: string;
  name: string;
  middleName: string;
  surName: string;
  email: string;
  phone: string;
  address: string;
  usuarioId: number;
}

export interface SelfClientUpdate{
  phone: string;
  address: string;
}
