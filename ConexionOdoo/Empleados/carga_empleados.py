import xml.etree.ElementTree as ET
import xmlrpc.client

try:
    tree = ET.parse("empleados.xml")
    root = tree.getroot()
except Exception as e:
    print("Error al leer empleados.xml:", e)
    exit(1)

try:
    url = "http://127.0.0.1:8071"
    db = "hotelsol"
    username = "esierrasanch@uoc.edu"
    password = "HotelSol123"

    common = xmlrpc.client.ServerProxy(f"{url}/xmlrpc/2/common")
    uid = common.authenticate(db, username, password, {})
    if not uid:
        raise Exception("Autenticacion fallida en Odoo")

    models = xmlrpc.client.ServerProxy(f"{url}/xmlrpc/2/object")
    departamentos_ids = {}

    for emp in root.findall("empleado"):
        nombre = emp.findtext("nombre", "").strip()
        email = emp.findtext("email", "").strip()
        movil = emp.findtext("movil", "").strip()
        depto = emp.findtext("departamento", "").strip()

        if not nombre or not depto:
            print("Empleado con datos incompletos. Omitido.")
            continue

        # Verificar o crear departamento
        if depto not in departamentos_ids:
            resultado = models.execute_kw(
                db, uid, password, 'hr.department', 'search',
                [[['name', '=', depto]]]
            )
            if not resultado:
                dept_id = models.execute_kw(
                    db, uid, password, 'hr.department', 'create',
                    [{'name': depto}]
                )
                print("Departamento creado:", depto)
            else:
                dept_id = resultado[0]
            departamentos_ids[depto] = dept_id
        else:
            dept_id = departamentos_ids[depto]

        # Evitar duplicados por nombre
        existe = models.execute_kw(
            db, uid, password, 'hr.employee', 'search',
            [[['name', '=', nombre]]]
        )
        if existe:
            print("Empleado ya existe:", nombre)
            continue

        emp_id = models.execute_kw(
            db, uid, password, 'hr.employee', 'create',
            [{
                'name': nombre,
                'work_email': email,
                'mobile_phone': movil,
                'department_id': dept_id
            }]
        )
        print("Empleado insertado:", nombre, "ID:", emp_id)

except Exception as e:
    print("Error durante la conexion o insercion en Odoo:", e)
