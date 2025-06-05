# Ensure you're in the solution root folder
Write-Host "Cleaning solution..." -ForegroundColor Cyan
dotnet.exe clean

# Step 1: Run tests and collect coverage
Write-Host "Running tests and collecting coverage..." -ForegroundColor Cyan
dotnet.exe test --collect:"XPlat Code Coverage" --results-directory ./TestResults

# Step 2: Find the most recent coverage report
$coverageFile = Get-ChildItem -Recurse -Filter "coverage.cobertura.xml" -Path "./TestResults" |
    Sort-Object LastWriteTime -Descending |
    Select-Object -First 1

if (-not $coverageFile) {
    Write-Error "❌ Coverage report not found!"
    exit 1
}

# Step 3: Install ReportGenerator if needed
if (-not (Get-Command reportgenerator -ErrorAction SilentlyContinue)) {
    Write-Host "Installing ReportGenerator..." -ForegroundColor Cyan
    dotnet.exe tool install -g dotnet-reportgenerator-globaltool --add-source https://api.nuget.org/v3/index.json -v q
    $env:Path += ";$env:USERPROFILE\.dotnet\tools"
}

# Step 4: Generate HTML report
Write-Host "Generating HTML coverage report..." -ForegroundColor Cyan
reportgenerator -reports:$coverageFile.FullName -targetdir:./coverage-report -reporttypes:Html

# Step 5: Open the report
$index = Join-Path $PWD "coverage-report\index.html"
Start-Process $index
