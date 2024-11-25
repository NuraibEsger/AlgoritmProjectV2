const apiBase = "http://api.currencylayer.com/live";
const apiKey = "a18b980c9aaf81b7753e8786873cdb34"; // Replace with your actual key

const currencyButtons = document.querySelectorAll('.currency-btn');
const amount1 = document.getElementById('amount1');
const amount2 = document.getElementById('amount2');
const messageBox = document.createElement('div'); // Create a message box for alerts
document.body.appendChild(messageBox);

let fromCurrency = 'RUB';
let toCurrency = 'USD';

messageBox.style.position = 'fixed';
messageBox.style.bottom = '10px';
messageBox.style.left = '50%';
messageBox.style.transform = 'translateX(-50%)';
messageBox.style.backgroundColor = '#ffcccc';
messageBox.style.color = '#333';
messageBox.style.padding = '10px 20px';
messageBox.style.borderRadius = '5px';
messageBox.style.boxShadow = '0 0 10px rgba(0,0,0,0.1)';
messageBox.style.display = 'none'; 

currencyButtons.forEach(button => {
  button.addEventListener('click', () => {
    const parent = button.parentElement;
    parent.querySelectorAll('.currency-btn').forEach(btn => btn.classList.remove('active'));
    button.classList.add('active');
    updateCurrencies();
  });
});

function updateCurrencies() {
  fromCurrency = document.querySelector('.converter-section:first-child .active').textContent;
  toCurrency = document.querySelector('.converter-section:last-child .active').textContent;
  convertCurrency();
}

function convertCurrency() {
  const inputAmount = parseFloat(amount1.value) || 0;

  if (fromCurrency === toCurrency) {
    amount2.value = inputAmount.toFixed(4);
    return;
  }

  if (!navigator.onLine) {
    showMessage('İnternet bağlantısı yoxdur. Zəhmət olmasa, bağlantınızı yoxlayın.');
    return;
  }

  fetch(`${apiBase}?access_key=${apiKey}&currencies=${toCurrency}&source=${fromCurrency}`)
    .then(response => {
      if (!response.ok) {
        throw new Error(`HTTP error! status: ${response.status}`);
      }
      return response.json();
    })
    .then(data => {
      if (data.success) {
        const exchangeRate = data.quotes[`${fromCurrency}${toCurrency}`];
        amount2.value = (inputAmount * exchangeRate).toFixed(4);
      } else {
        throw new Error(data.error.info || 'Failed to fetch exchange rates');
      }
    })
    .catch(error => {
      console.error('Error:', error);
      amount2.value = 'Error';
    });
}

function showMessage(message) {
  messageBox.textContent = message;
  messageBox.style.display = 'block';
  setTimeout(() => {
    messageBox.style.display = 'none';
  }, 5000);
}

window.addEventListener('offline', () => {
  showMessage('İnternet bağlantısı kəsildi. Yenidən bağlanmağa çalışın.');
});

window.addEventListener('online', () => {
  showMessage('İnternet bağlantısı bərpa edildi.');
});

amount1.addEventListener('input', convertCurrency);

convertCurrency();
