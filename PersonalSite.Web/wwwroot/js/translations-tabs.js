// translations-tabs.js
// Ensures Bootstrap nav-tabs work with browser history and persist active tab on reload

document.addEventListener('DOMContentLoaded', function () {
    // Find all nav-tabs
    document.querySelectorAll('.nav-tabs').forEach(function (tabs) {
        // Find all tab buttons
        const tabButtons = tabs.querySelectorAll('[data-bs-toggle="tab"]');
        tabButtons.forEach(function (btn) {
            btn.addEventListener('shown.bs.tab', function (event) {
                // Optionally, store the active tab in localStorage or history
                if (tabs.id) {
                    localStorage.setItem('activeTab-' + tabs.id, event.target.id);
                }
            });
        });
        // On load, restore active tab
        if (tabs.id) {
            const activeTabId = localStorage.getItem('activeTab-' + tabs.id);
            if (activeTabId) {
                const triggerEl = document.getElementById(activeTabId);
                if (triggerEl) {
                    new bootstrap.Tab(triggerEl).show();
                }
            }
        }
    });
});
