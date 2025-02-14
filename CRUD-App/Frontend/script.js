const API_BASE_URL = 'http://localhost:5000/api';
const form = document.getElementById('detailsForm');
const recordsTable = document.getElementById('recordsTable').getElementsByTagName('tbody')[0];

// State and District Data
const states = [
    { name: 'Karnataka', districts: ['Bangalore', 'Mysore', 'Mangalore'] },
    { name: 'Maharashtra', districts: ['Mumbai', 'Pune', 'Nagpur'] },
    { name: 'Tamil Nadu', districts: ['Chennai', 'Coimbatore', 'Madurai'] }
];

// Initialize form
document.addEventListener('DOMContentLoaded', () => {
    populateStates();
    loadRecords();
});

// Populate State dropdown
function populateStates() {
    const stateSelect = document.getElementById('state');
    stateSelect.innerHTML = '<option value="">Select State</option>';
    states.forEach(state => {
        const option = document.createElement('option');
        option.value = state.name;
        option.textContent = state.name;
        stateSelect.appendChild(option);
    });
}

// Handle State change to populate Districts
document.getElementById('state').addEventListener('change', (e) => {
    const selectedState = e.target.value;
    const districtSelect = document.getElementById('district');
    districtSelect.innerHTML = '<option value="">Select District</option>';
    
    if (selectedState) {
        const stateData = states.find(s => s.name === selectedState);
        stateData.districts.forEach(district => {
            const option = document.createElement('option');
            option.value = district;
            option.textContent = district;
            districtSelect.appendChild(option);
        });
    }
});

// Form Submission
form.addEventListener('submit', async (e) => {
    e.preventDefault();
    
    const formData = new FormData(form);
    const data = Object.fromEntries(formData.entries());
    
    try {
        const response = await fetch(`${API_BASE_URL}/records`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(data)
        });
        
        if (response.ok) {
            form.reset();
            loadRecords();
        } else {
            const error = await response.json();
            throw new Error(error.error || 'Failed to save record');
        }
    } catch (error) {
        console.error('Error:', error);
        alert(error.message);
    }
});

// Load Records
async function loadRecords() {
    try {
        const response = await fetch(`${API_BASE_URL}/records`);
        if (!response.ok) {
            throw new Error('Failed to load records');
        }
        const records = await response.json();
        renderTable(records);
    } catch (error) {
        console.error('Error loading records:', error);
        alert('Failed to load records');
    }
}

// Render Table
function renderTable(records) {
    recordsTable.innerHTML = '';
    records.forEach(record => {
        const row = document.createElement('tr');
        
        row.innerHTML = `
            <td>${record.name}</td>
            <td>${record.address}</td>
            <td>${record.state}</td>
            <td>${record.district}</td>
            <td>${new Date(record.dob).toLocaleDateString()}</td>
            <td>${record.language}</td>
            <td class="action-buttons">
                <button class="edit-btn" data-id="${record.id}">Edit</button>
                <button class="delete-btn" data-id="${record.id}">Delete</button>
            </td>
        `;
        
        recordsTable.appendChild(row);
    });

    // Add event listeners for edit and delete buttons
    document.querySelectorAll('.edit-btn').forEach(btn => {
        btn.addEventListener('click', handleEdit);
    });

    document.querySelectorAll('.delete-btn').forEach(btn => {
        btn.addEventListener('click', handleDelete);
    });
}

// Handle Edit
async function handleEdit(e) {
    const recordId = e.target.dataset.id;
    try {
        const response = await fetch(`${API_BASE_URL}/records/${recordId}`);
        if (!response.ok) {
            throw new Error('Failed to fetch record');
        }
        const record = await response.json();
        populateForm(record);
    } catch (error) {
        console.error('Error fetching record:', error);
        alert('Failed to fetch record');
    }
}

// Populate Form for Editing
function populateForm(record) {
    document.getElementById('name').value = record.name;
    document.getElementById('address').value = record.address;
    document.getElementById('state').value = record.state;
    document.getElementById('state').dispatchEvent(new Event('change'));
    setTimeout(() => {
        document.getElementById('district').value = record.district;
    }, 100);
    document.getElementById('dob').value = record.dob.split('T')[0];
    document.querySelector(`input[name="language"][value="${record.language}"]`).checked = true;
}

// Handle Delete
async function handleDelete(e) {
    const recordId = e.target.dataset.id;
    if (confirm('Are you sure you want to delete this record?')) {
        try {
            const response = await fetch(`${API_BASE_URL}/records/${recordId}`, {
                method: 'DELETE'
            });
            
            if (response.ok) {
                loadRecords();
            } else {
                const error = await response.json();
                throw new Error(error.error || 'Failed to delete record');
            }
        } catch (error) {
            console.error('Error:', error);
            alert(error.message);
        }
    }
}
