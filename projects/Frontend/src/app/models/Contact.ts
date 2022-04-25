export interface Contact {
    id: number;
    name: string;
    address: string;
    email: string;
    photo?: string;
    phoneNumbers: PhoneNumber[];
}

export interface PhoneNumber {
    number: string;
    label: string;
}
