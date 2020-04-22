from faker import Faker
import pyodbc

# Seed the Tenant table with random names. Work orders are 
# directly tied to tenants

# Steps: 
# Loop through rooms
# Generate Tenant name
# Insert row into database
def main():
    print("starting Tenant Seed script..")
    for x in range(1, 9):
        print('Generating Tenants for Floor {}'.format(x))
        room = 0
        for m in range(0, 16):
            if m < 10:
                room = '{}0{}'.format(x, m)
            else:
                room = '{}{}'.format(x, m)
        
            tenant = GetTenantName()
            tenant = tenant.split()
            
            InsertTenantRecord(room, tenant[0], tenant[1])

def InsertTenantRecord(room, first_name, last_name):
    sql = ("insert into tenant(FirstName, LastName, ApartmentNumber)"
        " values('{}', '{}', {})".format(first_name, last_name, room))
    conn = pyodbc.connect('driver={SQL Server}; server=192.168.1.134,49170;database=WorkManagement;uid=workmanagement;pwd=management1')
    cursor = conn.cursor()
    cursor.execute(sql)
    conn.commit()

def GetTenantName():
    fake = Faker()
    return '{} {}'.format(fake.first_name(), fake.last_name())

if __name__ == "__main__":
    main()