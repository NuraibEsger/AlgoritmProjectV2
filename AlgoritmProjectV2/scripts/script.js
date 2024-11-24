const currencyButtons = document.querySelectorAll('.currency-btn');
const amount1 = document.getElementById('amount1');
const amount2 = document.getElementById('amount2');

let fromCurrency = 'RUB';
let toCurrency = 'USD';

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

  fetch(`https://api.exchangerate.host/convert?from=${fromCurrency}&to=${toCurrency}&amount=${inputAmount}`)
    .then(response => response.json())
    .then(data => {
      amount2.value = data.result.toFixed(4);
    })
    .catch(() => {
      amount2.value = 'Error';
    });
}

amount1.addEventListener('input', convertCurrency);

convertCurrency();
