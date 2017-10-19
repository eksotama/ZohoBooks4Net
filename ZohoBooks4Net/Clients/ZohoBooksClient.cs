#region License
/*
 * Copyright 2017 Brandon James
 *
 *  Licensed under the Apache License, Version 2.0 (the "License");
 *  you may not use this file except in compliance with the License.
 *  You may obtain a copy of the License at
 *
 *      http://www.apache.org/licenses/LICENSE-2.0
 *
 *  Unless required by applicable law or agreed to in writing, software
 *  distributed under the License is distributed on an "AS IS" BASIS,
 *  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 *  See the License for the specific language governing permissions and
 *  limitations under the License.
 */
#endregion

namespace ZohoBooks4Net.Clients
{
    public class ZohoBooksClient
    {
        public readonly Configuration Configuration;

        public ZohoBooksClient(Configuration configuration)
        {
            Configuration = configuration;
        }

        private Authentication _Authentication;

        public Authentication Authentication
        {
            get
            {
                if (_Authentication == null)
                {
                    _Authentication = new Authentication(Configuration);
                }
                return _Authentication;
            }
        }

        private Contacts _Contacts;

        public Contacts Contacts
        {
            get
            {
                if (_Contacts == null)
                {
                    _Contacts = new Contacts(Configuration);
                }
                return _Contacts;
            }
        }

        private ContactPersons _ContactPersons;

        public ContactPersons ContactPersons
        {
            get
            {
                if (_ContactPersons == null)
                {
                    _ContactPersons = new ContactPersons(Configuration);
                }
                return _ContactPersons;
            }
        }

        private Estimates _Estimates;

        public Estimates Estimates
        {
            get
            {
                if (_Estimates == null)
                {
                    _Estimates = new Estimates(Configuration);
                }
                return _Estimates;
            }
        }

        private SalesOrders _SalesOrders;

        public SalesOrders SalesOrders
        {
            get
            {
                if (_SalesOrders != null)
                {
                    _SalesOrders = new SalesOrders(Configuration);
                }
                return _SalesOrders;
            }
        }

        private Invoices _Invoices;

        public Invoices Invoices
        {
            get
            {
                if (_Invoices == null)
                {
                    _Invoices = new Invoices(Configuration);
                }
                return _Invoices;
            }
        }

        private CustomerPayments _CustomerPayments;

        public CustomerPayments CustomerPayments
        {
            get
            {
                if (_CustomerPayments == null)
                {
                    _CustomerPayments = new CustomerPayments(Configuration);
                }
                return _CustomerPayments;
            }
        }

        private RetainerInvoices _RetainerInvoices;

        public RetainerInvoices RetainerInvoices
        {
            get
            {
                if (_RetainerInvoices == null)
                {
                    _RetainerInvoices = new RetainerInvoices(Configuration);
                }
                return _RetainerInvoices;
            }
        }

        private PurchaseOrders _PurchaseOrders;

        public PurchaseOrders PurchaseOrders
        {
            get
            {
                if (_PurchaseOrders == null)
                {
                    _PurchaseOrders = new PurchaseOrders(Configuration);
                }
                return _PurchaseOrders;
            }
        }

        private Users _Users;

        public Users Users
        {
            get
            {
                if (_Users == null)
                {
                    _Users = new Users(Configuration);
                }
                return _Users;
            }
        }

        private Items _Items;

        public Items Items
        {
            get
            {
                if (_Items == null)
                {
                    _Items = new Items(Configuration);
                }
                return _Items;
            }
        }

        private Journals _Journals;

        public Journals Journals
        {
            get
            {
                if (_Journals == null)
                {
                    _Journals = new Journals(Configuration);
                }
                return _Journals;
            }
        }

    }
}
